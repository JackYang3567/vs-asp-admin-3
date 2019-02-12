using Jiguang.JPush.Model;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
	public class JPushClient
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CSendPushAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string jsonBody;

			public HttpContent _003ChttpContent_003E5__1;

			public HttpResponseMessage _003Cmsg_003E5__2;

			public string _003Ccontent_003E5__3;

			public JPushClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter4;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter5;

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
						if (string.IsNullOrEmpty(jsonBody))
						{
							throw new ArgumentNullException("jsonBody");
						}
						_003ChttpContent_003E5__1 = new StringContent(jsonBody, Encoding.UTF8);
						awaiter2 = HttpClient.PostAsync("https://api.jpush.cn/v3/push", _003ChttpContent_003E5__1).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter4 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00b7;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter4;
						_003C_003Eu___0024awaiter4 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00b7;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter5;
							_003C_003Eu___0024awaiter5 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00b7:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__2 = result);
						awaiter = _003Cmsg_003E5__2.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter5 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003Ccontent_003E5__3 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__2.StatusCode, _003Cmsg_003E5__2.Headers, _003Ccontent_003E5__3);
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
		private struct _003CSendPushAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public JPushClient _003C_003E4__this;

			public PushPayload payload;

			public string _003Cbody_003E5__8;

			private TaskAwaiter<HttpResponse> _003C_003Eu___0024awaiter9;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					TaskAwaiter<HttpResponse> awaiter;
					if (_003C_003E1__state != 0)
					{
						if (payload == null)
						{
							throw new ArgumentNullException("payload");
						}
						_003Cbody_003E5__8 = payload.ToString();
						awaiter = _003C_003E4__this.SendPushAsync(_003Cbody_003E5__8).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter9 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter9;
						_003C_003Eu___0024awaiter9 = default(TaskAwaiter<HttpResponse>);
						_003C_003E1__state = -1;
					}
					HttpResponse result = awaiter.GetResult();
					awaiter = default(TaskAwaiter<HttpResponse>);
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
		private struct _003CIsPushValidAsync_003Ed__e : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string jsonBody;

			public HttpContent _003ChttpContent_003E5__f;

			public string _003Curl_003E5__10;

			public HttpResponseMessage _003Cmsg_003E5__11;

			public string _003Ccontent_003E5__12;

			public JPushClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter13;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter14;

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
						if (string.IsNullOrEmpty(jsonBody))
						{
							throw new ArgumentNullException("jsonBody");
						}
						_003ChttpContent_003E5__f = new StringContent(jsonBody, Encoding.UTF8);
						_003Curl_003E5__10 = "https://api.jpush.cn/v3/push/validate";
						awaiter2 = HttpClient.PostAsync(_003Curl_003E5__10, _003ChttpContent_003E5__f).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter13 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00c3;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter13;
						_003C_003Eu___0024awaiter13 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00c3;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter14;
							_003C_003Eu___0024awaiter14 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00c3:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__11 = result);
						awaiter = _003Cmsg_003E5__11.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter14 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003Ccontent_003E5__12 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__11.StatusCode, _003Cmsg_003E5__11.Headers, _003Ccontent_003E5__12);
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
		private struct _003CIsPushValidAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public JPushClient _003C_003E4__this;

			public PushPayload payload;

			public string _003Cbody_003E5__17;

			private TaskAwaiter<HttpResponse> _003C_003Eu___0024awaiter18;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					TaskAwaiter<HttpResponse> awaiter;
					if (_003C_003E1__state != 0)
					{
						if (payload == null)
						{
							throw new ArgumentNullException("payload");
						}
						_003Cbody_003E5__17 = payload.ToString();
						awaiter = _003C_003E4__this.IsPushValidAsync(_003Cbody_003E5__17).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter18 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter18;
						_003C_003Eu___0024awaiter18 = default(TaskAwaiter<HttpResponse>);
						_003C_003E1__state = -1;
					}
					HttpResponse result = awaiter.GetResult();
					awaiter = default(TaskAwaiter<HttpResponse>);
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
		private struct _003CGetCIdListAsync_003Ed__1d : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public int? count;

			public string type;

			public string _003Curl_003E5__1e;

			public HttpResponseMessage _003Cmsg_003E5__1f;

			public string _003Ccontent_003E5__20;

			public JPushClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter21;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter22;

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
						if (count.HasValue && count < 1 && count > 1000)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						_003Curl_003E5__1e = "https://api.jpush.cn/v3/push/cid";
						if (count.HasValue)
						{
							_003Curl_003E5__1e = _003Curl_003E5__1e + "?count=" + count;
							if (!string.IsNullOrEmpty(type))
							{
								_003Curl_003E5__1e = _003Curl_003E5__1e + "&type=" + type;
							}
						}
						awaiter2 = HttpClient.GetAsync(_003Curl_003E5__1e).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter21 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_013e;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter21;
						_003C_003Eu___0024awaiter21 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_013e;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter22;
							_003C_003Eu___0024awaiter22 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_013e:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__1f = result);
						awaiter = _003Cmsg_003E5__1f.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter22 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003Ccontent_003E5__20 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__1f.StatusCode, _003Cmsg_003E5__1f.Headers, _003Ccontent_003E5__20);
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

		private const string BASE_URL = "https://api.jpush.cn/v3/push";

		public DeviceClient Device;

		public ScheduleClient Schedule;

		private ReportClient report;

		public static readonly HttpClient HttpClient;

		public ReportClient Report
		{
			get;
			set;
		}

		static JPushClient()
		{
			HttpClient = new HttpClient();
			HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public JPushClient(string appKey, string masterSecret)
		{
			if (string.IsNullOrEmpty(appKey))
			{
				throw new ArgumentNullException("appKey");
			}
			if (string.IsNullOrEmpty(masterSecret))
			{
				throw new ArgumentNullException("masterSecret");
			}
			string parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", parameter);
			Report = new ReportClient();
			Device = new DeviceClient();
			Schedule = new ScheduleClient();
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CSendPushAsync_003Ed__0))]
		public Task<HttpResponse> SendPushAsync(string jsonBody)
		{
			_003CSendPushAsync_003Ed__0 stateMachine = default(_003CSendPushAsync_003Ed__0);
			stateMachine._003C_003E4__this = this;
			stateMachine.jsonBody = jsonBody;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CSendPushAsync_003Ed__7))]
		public Task<HttpResponse> SendPushAsync(PushPayload payload)
		{
			_003CSendPushAsync_003Ed__7 stateMachine = default(_003CSendPushAsync_003Ed__7);
			stateMachine._003C_003E4__this = this;
			stateMachine.payload = payload;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse SendPush(PushPayload pushPayload)
		{
			Task<HttpResponse> task = Task.Run(() => SendPushAsync(pushPayload));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CIsPushValidAsync_003Ed__e))]
		[DebuggerStepThrough]
		public Task<HttpResponse> IsPushValidAsync(string jsonBody)
		{
			_003CIsPushValidAsync_003Ed__e stateMachine = default(_003CIsPushValidAsync_003Ed__e);
			stateMachine._003C_003E4__this = this;
			stateMachine.jsonBody = jsonBody;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CIsPushValidAsync_003Ed__16))]
		[DebuggerStepThrough]
		public Task<HttpResponse> IsPushValidAsync(PushPayload payload)
		{
			_003CIsPushValidAsync_003Ed__16 stateMachine = default(_003CIsPushValidAsync_003Ed__16);
			stateMachine._003C_003E4__this = this;
			stateMachine.payload = payload;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse IsPushValid(PushPayload pushPayload)
		{
			Task<HttpResponse> task = Task.Run(() => IsPushValidAsync(pushPayload));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CGetCIdListAsync_003Ed__1d))]
		public Task<HttpResponse> GetCIdListAsync(int? count, string type)
		{
			_003CGetCIdListAsync_003Ed__1d stateMachine = default(_003CGetCIdListAsync_003Ed__1d);
			stateMachine._003C_003E4__this = this;
			stateMachine.count = count;
			stateMachine.type = type;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetCIdList(int? count, string type)
		{
			Task<HttpResponse> task = Task.Run(() => GetCIdListAsync(count, type));
			task.Wait();
			return task.Result;
		}
	}
}
