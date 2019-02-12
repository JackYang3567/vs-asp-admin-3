using Jiguang.JPush.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
	public class ReportClient
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CGetMessageReportAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public List<string> msgIdList;

			public string _003CmsgIds_003E5__1;

			public string _003Curl_003E5__2;

			public HttpResponseMessage _003Cmsg_003E5__3;

			public string _003Ccontent_003E5__4;

			public ReportClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter5;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter6;

			private void MoveNext()
			{
				HttpResponse result3;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					HttpResponseMessage result;
					HttpResponseMessage httpResponseMessage;
					switch (_003C_003E1__state)
					{
					default:
						if (msgIdList == null)
						{
							throw new ArgumentNullException("msgIdList");
						}
						_003CmsgIds_003E5__1 = string.Join(",", msgIdList);
						_003Curl_003E5__2 = "https://report.jpush.cn/v3/received?msg_ids=" + _003CmsgIds_003E5__1;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__2).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter5 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00c3;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter5;
						_003C_003Eu___0024awaiter5 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00c3;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter6;
							_003C_003Eu___0024awaiter6 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00c3:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__3 = result);
						awaiter = _003Cmsg_003E5__3.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter6 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003Ccontent_003E5__4 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__3.StatusCode, _003Cmsg_003E5__3.Headers, _003Ccontent_003E5__4);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result3);
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine param0)
			{
				_003C_003Et__builder.SetStateMachine(param0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(param0);
			}
		}

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CGetMessageDetailReportAsync_003Ed__b : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public List<string> msgIdList;

			public string _003CmsgIds_003E5__c;

			public string _003Curl_003E5__d;

			public HttpResponseMessage _003Cmsg_003E5__e;

			public string _003Ccontent_003E5__f;

			public ReportClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter10;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter11;

			private void MoveNext()
			{
				HttpResponse result3;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					HttpResponseMessage result;
					HttpResponseMessage httpResponseMessage;
					switch (_003C_003E1__state)
					{
					default:
						if (msgIdList == null)
						{
							throw new ArgumentNullException("msgIdList");
						}
						_003CmsgIds_003E5__c = string.Join(",", msgIdList);
						_003Curl_003E5__d = "https://report.jpush.cn/v3/messages?msg_ids=" + _003CmsgIds_003E5__c;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__d).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter10 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00c3;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter10;
						_003C_003Eu___0024awaiter10 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00c3;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter11;
							_003C_003Eu___0024awaiter11 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00c3:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__e = result);
						awaiter = _003Cmsg_003E5__e.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter11 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003Ccontent_003E5__f = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__e.StatusCode, _003Cmsg_003E5__e.Headers, _003Ccontent_003E5__f);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result3);
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine param0)
			{
				_003C_003Et__builder.SetStateMachine(param0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(param0);
			}
		}

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CGetUserReportAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string timeUnit;

			public string startTime;

			public int duration;

			public string _003Curl_003E5__17;

			public HttpResponseMessage _003Cmsg_003E5__18;

			public string _003Ccontent_003E5__19;

			public ReportClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter1a;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter1b;

			private void MoveNext()
			{
				HttpResponse result3;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					HttpResponseMessage result;
					HttpResponseMessage httpResponseMessage;
					switch (_003C_003E1__state)
					{
					default:
						if (string.IsNullOrEmpty(timeUnit))
						{
							throw new ArgumentNullException("timeUnit");
						}
						if (startTime == null)
						{
							throw new ArgumentNullException("startTime");
						}
						if (duration <= 0)
						{
							throw new ArgumentOutOfRangeException("duration");
						}
						_003Curl_003E5__17 = "https://report.jpush.cn/v3/users?time_unit=" + timeUnit + "&start=" + startTime + "&duration=" + duration;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__17).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter1a = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_0116;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter1a;
						_003C_003Eu___0024awaiter1a = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_0116;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter1b;
							_003C_003Eu___0024awaiter1b = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_0116:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__18 = result);
						awaiter = _003Cmsg_003E5__18.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter1b = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003Ccontent_003E5__19 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__18.StatusCode, _003Cmsg_003E5__18.Headers, _003Ccontent_003E5__19);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result3);
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine param0)
			{
				_003C_003Et__builder.SetStateMachine(param0);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine param0)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(param0);
			}
		}

		private const string BASE_URL = "https://report.jpush.cn/v3/";

		[AsyncStateMachine(typeof(_003CGetMessageReportAsync_003Ed__0))]
		[DebuggerStepThrough]
		public Task<HttpResponse> GetMessageReportAsync(List<string> msgIdList)
		{
			_003CGetMessageReportAsync_003Ed__0 stateMachine = default(_003CGetMessageReportAsync_003Ed__0);
			stateMachine._003C_003E4__this = this;
			stateMachine.msgIdList = msgIdList;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetMessageReport(List<string> msgIdList)
		{
			Task<HttpResponse> task = Task.Run(() => GetMessageReportAsync(msgIdList));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CGetMessageDetailReportAsync_003Ed__b))]
		public Task<HttpResponse> GetMessageDetailReportAsync(List<string> msgIdList)
		{
			_003CGetMessageDetailReportAsync_003Ed__b stateMachine = default(_003CGetMessageDetailReportAsync_003Ed__b);
			stateMachine._003C_003E4__this = this;
			stateMachine.msgIdList = msgIdList;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetMessageDetailReport(List<string> msgIdList)
		{
			Task<HttpResponse> task = Task.Run(() => GetMessageDetailReportAsync(msgIdList));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CGetUserReportAsync_003Ed__16))]
		public Task<HttpResponse> GetUserReportAsync(string timeUnit, string startTime, int duration)
		{
			_003CGetUserReportAsync_003Ed__16 stateMachine = default(_003CGetUserReportAsync_003Ed__16);
			stateMachine._003C_003E4__this = this;
			stateMachine.timeUnit = timeUnit;
			stateMachine.startTime = startTime;
			stateMachine.duration = duration;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetUserReport(string timeUnit, string startTime, int duration)
		{
			Task<HttpResponse> task = Task.Run(() => GetUserReportAsync(timeUnit, startTime, duration));
			task.Wait();
			return task.Result;
		}
	}
}
