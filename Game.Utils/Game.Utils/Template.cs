using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace Game.Utils
{
	public class Template
	{
		private Regex variableGrammarRegex = new Regex("{#= \\w+?#}", RegexOptions.Compiled | RegexOptions.Singleline);

		private Regex forStatementGrammarRegex = new Regex("{# for \\w+ in \\w+#}.*?{# endfor#}", RegexOptions.Compiled | RegexOptions.Singleline);

		private string _templateCode;

		private Dictionary<string, object> _variableDataScoureList;

		private Dictionary<string, DataTable> _forDataScoureList;

		public string TemplateCode
		{
			get
			{
				return _templateCode;
			}
			set
			{
				_templateCode = value;
			}
		}

		public Dictionary<string, object> VariableDataScoureList
		{
			get
			{
				return _variableDataScoureList;
			}
			set
			{
				_variableDataScoureList = value;
			}
		}

		public Dictionary<string, DataTable> ForDataScoureList
		{
			get
			{
				return _forDataScoureList;
			}
			set
			{
				_forDataScoureList = value;
			}
		}

		public Template(string templatePath)
		{
			string file = HttpContext.Current.Server.MapPath(templatePath);
			_templateCode = FileManager.ReadFile(file);
			_variableDataScoureList = new Dictionary<string, object>();
			_forDataScoureList = new Dictionary<string, DataTable>();
		}

		public string OutputHTML()
		{
			if (_variableDataScoureList.Count > 0)
			{
				foreach (Match item in variableGrammarRegex.Matches(_templateCode))
				{
					string key = item.Value.Replace("{#= ", "").Replace("#}", "");
					_templateCode = _templateCode.Replace(item.Value, _variableDataScoureList[key].ToString());
				}
			}
			if (_forDataScoureList.Count > 0)
			{
				Regex regex = new Regex("{# for \\w+? in", RegexOptions.Compiled);
				Regex regex2 = new Regex("{# for {1}\\w+? in \\w+?#}", RegexOptions.Compiled);
				string text = string.Empty;
				foreach (Match item2 in forStatementGrammarRegex.Matches(_templateCode))
				{
					Match match3 = regex2.Match(item2.Value);
					Match match4 = regex.Match(item2.Value);
					if (match3.Success && match4.Success)
					{
						string text2 = match4.Value.Replace("{# for ", "").Replace(" in", "");
						string text3 = match3.Value.Replace("{# for ", "").Replace(text2, "").Replace(" in ", "")
							.Replace("#}", "");
						string text4 = item2.Value.Replace("{# endfor#}", "").Replace("{# for " + text2 + " in " + text3 + "#}", "");
						Regex regex3 = new Regex("{# " + text2 + ".\\w+?#}", RegexOptions.Compiled);
						DataTable dataTable = _forDataScoureList[text3];
						foreach (DataRow row in dataTable.Rows)
						{
							string text5 = text4;
							foreach (Match item3 in regex3.Matches(text4))
							{
								string columnName = item3.Value.Replace("{# " + text2 + ".", "").Replace("#}", "");
								text5 = text5.Replace(item3.Value, row[columnName].ToString());
							}
							text += text5;
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						_templateCode = _templateCode.Replace(item2.Value, text);
					}
					text = string.Empty;
				}
			}
			return _templateCode;
		}
	}
}
