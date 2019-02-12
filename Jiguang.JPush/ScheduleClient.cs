using Game.Facade;
using Jiguang.JPush.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
	public class ScheduleClient
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CCreateScheduleTaskAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string json;

			public string _003Curl_003E5__1;

			public HttpContent _003CrequestContent_003E5__2;

			public HttpResponseMessage _003Cmsg_003E5__3;

			public string _003CresponseContent_003E5__4;

			public ScheduleClient _003C_003E4__this;

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
						if (string.IsNullOrEmpty(json))
						{
							throw new ArgumentNullException("json");
						}
						_003Curl_003E5__1 = "https://api.jpush.cn/v3/schedules";
						_003CrequestContent_003E5__2 = new StringContent(json, Encoding.UTF8);
						awaiter2 = JPushClient.HttpClient.PostAsync(_003Curl_003E5__1, _003CrequestContent_003E5__2).ConfigureAwait(false).GetAwaiter();
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
					string text = _003CresponseContent_003E5__4 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__3.StatusCode, _003Cmsg_003E5__3.Headers, _003CresponseContent_003E5__4);
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
		private struct _003CCreateSingleScheduleTaskAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public ScheduleClient _003C_003E4__this;

			public string name;

			public PushPayload pushPayload;

			public string triggeringTime;

			public object _003Cobj_003E5__9;

			private ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter _003C_003Eu___0024awaitera;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(name))
						{
							throw new ArgumentNullException("name");
						}
						if (pushPayload == null)
						{
							throw new ArgumentNullException("pushPayload");
						}
						if (string.IsNullOrEmpty(triggeringTime))
						{
							throw new ArgumentNullException("triggeringTime");
						}
						_003Cobj_003E5__9 = new
						{
							name = name,
							enabled = true,
							push = JObject.FromObject(pushPayload),
							trigger = new
							{
								single = new
								{
									time = triggeringTime
								}
							}
						};
						awaiter = _003C_003E4__this.CreateScheduleTaskAsync(JsonHelper.SerializeObject(_003Cobj_003E5__9)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaitera = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaitera;
						_003C_003Eu___0024awaitera = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponse result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
					result2 = result;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CCreatePeriodicalScheduleTaskAsync_003Ed__f : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public ScheduleClient _003C_003E4__this;

			public string name;

			public PushPayload pushPayload;

			public Trigger trigger;

			public JObject _003CrequestJson_003E5__10;

			private ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter11;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(name))
						{
							throw new ArgumentNullException("name");
						}
						if (pushPayload == null)
						{
							throw new ArgumentNullException("pushPayload");
						}
						if (trigger == null)
						{
							throw new ArgumentNullException("trigger");
						}
						_003CrequestJson_003E5__10 = new JObject();
						_003CrequestJson_003E5__10["name"] = name;
						_003CrequestJson_003E5__10["enabled"] = true;
						_003CrequestJson_003E5__10["push"] = JObject.FromObject(pushPayload);
						_003CrequestJson_003E5__10["trigger"] = new JObject();
						_003CrequestJson_003E5__10["trigger"]["periodical"] = JObject.FromObject(trigger);
						awaiter = _003C_003E4__this.CreateScheduleTaskAsync(_003CrequestJson_003E5__10.ToString()).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter11 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter11;
						_003C_003Eu___0024awaiter11 = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponse result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
					result2 = result;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CGetValidScheduleTasksAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public int page;

			public string _003Curl_003E5__17;

			public HttpResponseMessage _003Cmsg_003E5__18;

			public string _003CresponseContent_003E5__19;

			public ScheduleClient _003C_003E4__this;

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
						if (page <= 0)
						{
							throw new ArgumentNullException("page");
						}
						_003Curl_003E5__17 = "https://api.jpush.cn/v3/schedules?page=" + page;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__17).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter1a = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00b3;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter1a;
						_003C_003Eu___0024awaiter1a = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00b3;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter1b;
							_003C_003Eu___0024awaiter1b = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00b3:
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
					string text = _003CresponseContent_003E5__19 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__18.StatusCode, _003Cmsg_003E5__18.Headers, _003CresponseContent_003E5__19);
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
		private struct _003CGetScheduleTaskAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string scheduleId;

			public string _003Curl_003E5__21;

			public HttpResponseMessage _003Cmsg_003E5__22;

			public string _003CresponseContent_003E5__23;

			public ScheduleClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter24;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter25;

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
						if (string.IsNullOrEmpty(scheduleId))
						{
							throw new ArgumentNullException("scheduleId");
						}
						_003Curl_003E5__21 = "https://api.jpush.cn/v3/schedules/" + scheduleId;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__21).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter24 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00b2;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter24;
						_003C_003Eu___0024awaiter24 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00b2;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter25;
							_003C_003Eu___0024awaiter25 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00b2:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__22 = result);
						awaiter = _003Cmsg_003E5__22.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter25 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseContent_003E5__23 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__22.StatusCode, _003Cmsg_003E5__22.Headers, _003CresponseContent_003E5__23);
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
		private struct _003CUpdateScheduleTaskAsync_003Ed__2a : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string scheduleId;

			public string json;

			public string _003Curl_003E5__2b;

			public HttpContent _003CrequestContent_003E5__2c;

			public HttpResponseMessage _003Cmsg_003E5__2d;

			public string _003CresponseContent_003E5__2e;

			public ScheduleClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter2f;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter30;

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
						if (string.IsNullOrEmpty(scheduleId))
						{
							throw new ArgumentNullException("scheduleId");
						}
						if (string.IsNullOrEmpty(json))
						{
							throw new ArgumentNullException("json");
						}
						_003Curl_003E5__2b = "https://api.jpush.cn/v3/schedules/" + scheduleId;
						_003CrequestContent_003E5__2c = new StringContent(json, Encoding.UTF8);
						awaiter2 = JPushClient.HttpClient.PutAsync(_003Curl_003E5__2b, _003CrequestContent_003E5__2c).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter2f = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00e6;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter2f;
						_003C_003Eu___0024awaiter2f = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00e6;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter30;
							_003C_003Eu___0024awaiter30 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00e6:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__2d = result);
						awaiter = _003Cmsg_003E5__2d.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter30 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseContent_003E5__2e = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__2d.StatusCode, _003Cmsg_003E5__2d.Headers, _003CresponseContent_003E5__2e);
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
		private struct _003CUpdateSingleScheduleTaskAsync_003Ed__32 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public ScheduleClient _003C_003E4__this;

			public string scheduleId;

			public string name;

			public bool? enabled;

			public string triggeringTime;

			public PushPayload pushPayload;

			public JObject _003Cjson_003E5__33;

			private ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter34;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(scheduleId))
						{
							throw new ArgumentNullException(scheduleId);
						}
						_003Cjson_003E5__33 = new JObject();
						if (!string.IsNullOrEmpty(name))
						{
							_003Cjson_003E5__33["name"] = name;
						}
						if (enabled.HasValue)
						{
							_003Cjson_003E5__33["enabled"] = enabled;
						}
						if (triggeringTime != null)
						{
							_003Cjson_003E5__33["trigger"] = new JObject();
							_003Cjson_003E5__33["trigger"]["single"] = new JObject();
							_003Cjson_003E5__33["trigger"]["single"]["time"] = triggeringTime;
						}
						if (pushPayload != null)
						{
							_003Cjson_003E5__33["push"] = JObject.FromObject(pushPayload);
						}
						awaiter = _003C_003E4__this.UpdateScheduleTaskAsync(scheduleId, _003Cjson_003E5__33.ToString()).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter34 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter34;
						_003C_003Eu___0024awaiter34 = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponse result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
					result2 = result;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CUpdatePeriodicalScheduleTaskAsync_003Ed__39 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public ScheduleClient _003C_003E4__this;

			public string scheduleId;

			public string name;

			public bool? enabled;

			public Trigger trigger;

			public PushPayload pushPayload;

			public JObject _003Cjson_003E5__3a;

			private ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter3b;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(scheduleId))
						{
							throw new ArgumentNullException(scheduleId);
						}
						_003Cjson_003E5__3a = new JObject();
						if (!string.IsNullOrEmpty(name))
						{
							_003Cjson_003E5__3a["name"] = name;
						}
						if (enabled.HasValue)
						{
							_003Cjson_003E5__3a["enabled"] = enabled;
						}
						if (trigger != null)
						{
							_003Cjson_003E5__3a["trigger"] = new JObject();
							_003Cjson_003E5__3a["trigger"]["periodical"] = JObject.FromObject(trigger);
						}
						if (pushPayload != null)
						{
							_003Cjson_003E5__3a["push"] = JObject.FromObject(pushPayload);
						}
						awaiter = _003C_003E4__this.UpdateScheduleTaskAsync(scheduleId, _003Cjson_003E5__3a.ToString()).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter3b = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter3b;
						_003C_003Eu___0024awaiter3b = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponse result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponse>.ConfiguredTaskAwaiter);
					result2 = result;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CDeleteScheduleTaskAsync_003Ed__40 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string scheduleId;

			public string _003Curl_003E5__41;

			public HttpResponseMessage _003Cmsg_003E5__42;

			public string _003CresponseContent_003E5__43;

			public ScheduleClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter44;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter45;

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
						if (string.IsNullOrEmpty(scheduleId))
						{
							throw new ArgumentNullException("scheduleId");
						}
						_003Curl_003E5__41 = "https://api.jpush.cn/v3/schedules/" + scheduleId;
						awaiter2 = JPushClient.HttpClient.DeleteAsync(_003Curl_003E5__41).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter44 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00b2;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter44;
						_003C_003Eu___0024awaiter44 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00b2;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter45;
							_003C_003Eu___0024awaiter45 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00b2:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__42 = result);
						awaiter = _003Cmsg_003E5__42.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter45 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseContent_003E5__43 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__42.StatusCode, _003Cmsg_003E5__42.Headers, _003CresponseContent_003E5__43);
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

		private const string BASE_URL = "https://api.jpush.cn";

		[AsyncStateMachine(typeof(_003CCreateScheduleTaskAsync_003Ed__0))]
		[DebuggerStepThrough]
		public Task<HttpResponse> CreateScheduleTaskAsync(string json)
		{
			_003CCreateScheduleTaskAsync_003Ed__0 stateMachine = default(_003CCreateScheduleTaskAsync_003Ed__0);
			stateMachine._003C_003E4__this = this;
			stateMachine.json = json;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CCreateSingleScheduleTaskAsync_003Ed__8))]
		public Task<HttpResponse> CreateSingleScheduleTaskAsync(string name, PushPayload pushPayload, string triggeringTime)
		{
			_003CCreateSingleScheduleTaskAsync_003Ed__8 stateMachine = default(_003CCreateSingleScheduleTaskAsync_003Ed__8);
			stateMachine._003C_003E4__this = this;
			stateMachine.name = name;
			stateMachine.pushPayload = pushPayload;
			stateMachine.triggeringTime = triggeringTime;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse CreateSingleScheduleTask(string name, PushPayload pushPayload, string triggeringTime)
		{
			Task<HttpResponse> task = Task.Run(() => CreateSingleScheduleTaskAsync(name, pushPayload, triggeringTime));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CCreatePeriodicalScheduleTaskAsync_003Ed__f))]
		[DebuggerStepThrough]
		public Task<HttpResponse> CreatePeriodicalScheduleTaskAsync(string name, PushPayload pushPayload, Trigger trigger)
		{
			_003CCreatePeriodicalScheduleTaskAsync_003Ed__f stateMachine = default(_003CCreatePeriodicalScheduleTaskAsync_003Ed__f);
			stateMachine._003C_003E4__this = this;
			stateMachine.name = name;
			stateMachine.pushPayload = pushPayload;
			stateMachine.trigger = trigger;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse CreatePeriodicalScheduleTask(string name, PushPayload pushPayload, Trigger trigger)
		{
			Task<HttpResponse> task = Task.Run(() => CreatePeriodicalScheduleTaskAsync(name, pushPayload, trigger));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CGetValidScheduleTasksAsync_003Ed__16))]
		[DebuggerStepThrough]
		public Task<HttpResponse> GetValidScheduleTasksAsync(int page = 1)
		{
			_003CGetValidScheduleTasksAsync_003Ed__16 stateMachine = default(_003CGetValidScheduleTasksAsync_003Ed__16);
			stateMachine._003C_003E4__this = this;
			stateMachine.page = page;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetValidScheduleTasks(int page = 1)
		{
			Task<HttpResponse> task = Task.Run(() => GetValidScheduleTasksAsync(page));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CGetScheduleTaskAsync_003Ed__20))]
		public Task<HttpResponse> GetScheduleTaskAsync(string scheduleId)
		{
			_003CGetScheduleTaskAsync_003Ed__20 stateMachine = default(_003CGetScheduleTaskAsync_003Ed__20);
			stateMachine._003C_003E4__this = this;
			stateMachine.scheduleId = scheduleId;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetScheduleTask(string scheduleId)
		{
			Task<HttpResponse> task = Task.Run(() => GetScheduleTaskAsync(scheduleId));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CUpdateScheduleTaskAsync_003Ed__2a))]
		[DebuggerStepThrough]
		public Task<HttpResponse> UpdateScheduleTaskAsync(string scheduleId, string json)
		{
			_003CUpdateScheduleTaskAsync_003Ed__2a stateMachine = default(_003CUpdateScheduleTaskAsync_003Ed__2a);
			stateMachine._003C_003E4__this = this;
			stateMachine.scheduleId = scheduleId;
			stateMachine.json = json;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CUpdateSingleScheduleTaskAsync_003Ed__32))]
		[DebuggerStepThrough]
		public Task<HttpResponse> UpdateSingleScheduleTaskAsync(string scheduleId, string name, bool? enabled, string triggeringTime, PushPayload pushPayload)
		{
			_003CUpdateSingleScheduleTaskAsync_003Ed__32 stateMachine = default(_003CUpdateSingleScheduleTaskAsync_003Ed__32);
			stateMachine._003C_003E4__this = this;
			stateMachine.scheduleId = scheduleId;
			stateMachine.name = name;
			stateMachine.enabled = enabled;
			stateMachine.triggeringTime = triggeringTime;
			stateMachine.pushPayload = pushPayload;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse UpdateSingleScheduleTask(string scheduleId, string name, bool? enabled, string triggeringTime, PushPayload pushPayload)
		{
			Task<HttpResponse> task = Task.Run(() => UpdateSingleScheduleTaskAsync(scheduleId, name, enabled, triggeringTime, pushPayload));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CUpdatePeriodicalScheduleTaskAsync_003Ed__39))]
		[DebuggerStepThrough]
		public Task<HttpResponse> UpdatePeriodicalScheduleTaskAsync(string scheduleId, string name, bool? enabled, Trigger trigger, PushPayload pushPayload)
		{
			_003CUpdatePeriodicalScheduleTaskAsync_003Ed__39 stateMachine = default(_003CUpdatePeriodicalScheduleTaskAsync_003Ed__39);
			stateMachine._003C_003E4__this = this;
			stateMachine.scheduleId = scheduleId;
			stateMachine.name = name;
			stateMachine.enabled = enabled;
			stateMachine.trigger = trigger;
			stateMachine.pushPayload = pushPayload;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse UpdatePeriodicalScheduleTask(string scheduleId, string name, bool? enabled, Trigger trigger, PushPayload pushPayload)
		{
			Task<HttpResponse> task = Task.Run(() => UpdatePeriodicalScheduleTaskAsync(scheduleId, name, enabled, trigger, pushPayload));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CDeleteScheduleTaskAsync_003Ed__40))]
		public Task<HttpResponse> DeleteScheduleTaskAsync(string scheduleId)
		{
			_003CDeleteScheduleTaskAsync_003Ed__40 stateMachine = default(_003CDeleteScheduleTaskAsync_003Ed__40);
			stateMachine._003C_003E4__this = this;
			stateMachine.scheduleId = scheduleId;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse DeleteScheduleTask(string scheduleId)
		{
			Task<HttpResponse> task = Task.Run(() => DeleteScheduleTaskAsync(scheduleId));
			task.Wait();
			return task.Result;
		}
	}
}
