using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Admin.Models
{
	public class ChartResult : ActionResult
	{
		private readonly Chart _chart;

		private readonly string _format;

		public Chart Chart
		{
			get
			{
				return _chart;
			}
		}

		public string Format
		{
			get
			{
				return _format;
			}
		}

		public ChartResult(Chart chart, string format = "png")
		{
			if (chart == null)
			{
				throw new ArgumentNullException("chart");
			}
			_chart = chart;
			_format = format;
			if (string.IsNullOrEmpty(_format))
			{
				_format = "png";
			}
		}

		public override void ExecuteResult(ControllerContext context)
		{
			_chart.Write(_format);
		}
	}
}
