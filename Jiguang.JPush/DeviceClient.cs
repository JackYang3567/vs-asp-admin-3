using Jiguang.JPush.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JPush
{
	public class DeviceClient
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CGetDeviceInfoAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string registrationId;

			public string _003Curl_003E5__1;

			public HttpResponseMessage _003Cmsg_003E5__2;

			public string _003Ccontent_003E5__3;

			public DeviceClient _003C_003E4__this;

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
						if (string.IsNullOrEmpty(registrationId))
						{
							throw new ArgumentNullException(registrationId);
						}
						_003Curl_003E5__1 = "https://device.jpush.cn/v3/devices/" + registrationId;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__1).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter4 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00b3;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter4;
						_003C_003Eu___0024awaiter4 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00b3;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter5;
							_003C_003Eu___0024awaiter5 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00b3:
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
		private struct _003CUpdateDeviceInfoAsync_003Ed__a : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string registrationId;

			public string json;

			public string _003Curl_003E5__b;

			public HttpContent _003CrequestContent_003E5__c;

			public HttpResponseMessage _003Cmsg_003E5__d;

			public string _003CresponseContent_003E5__e;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiterf;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter10;

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
						if (string.IsNullOrEmpty(registrationId))
						{
							throw new ArgumentNullException("registrationId");
						}
						if (string.IsNullOrEmpty(json))
						{
							throw new ArgumentNullException("json");
						}
						_003Curl_003E5__b = "https://device.jpush.cn/v3/devices/" + registrationId;
						_003CrequestContent_003E5__c = new StringContent(json, Encoding.UTF8);
						awaiter2 = JPushClient.HttpClient.PostAsync(_003Curl_003E5__b, _003CrequestContent_003E5__c).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiterf = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00e6;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiterf;
						_003C_003Eu___0024awaiterf = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00e6;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter10;
							_003C_003Eu___0024awaiter10 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00e6:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__d = result);
						awaiter = _003Cmsg_003E5__d.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter10 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseContent_003E5__e = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__d.StatusCode, _003Cmsg_003E5__d.Headers, _003CresponseContent_003E5__e);
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
		private struct _003CUpdateDeviceInfoAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public DeviceClient _003C_003E4__this;

			public string registrationId;

			public DevicePayload devicePayload;

			public string _003Cjson_003E5__13;

			private TaskAwaiter<HttpResponse> _003C_003Eu___0024awaiter14;

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
						if (string.IsNullOrEmpty(registrationId))
						{
							throw new ArgumentNullException("registrationId");
						}
						if (devicePayload == null)
						{
							throw new ArgumentNullException("devicePayload");
						}
						_003Cjson_003E5__13 = devicePayload.ToString();
						awaiter = _003C_003E4__this.UpdateDeviceInfoAsync(registrationId, _003Cjson_003E5__13).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter14 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter14;
						_003C_003Eu___0024awaiter14 = default(TaskAwaiter<HttpResponse>);
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
		private struct _003CGetDevicesByAliasAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string alias;

			public string platform;

			public string _003Curl_003E5__1a;

			public HttpResponseMessage _003Cmsg_003E5__1b;

			public string _003CresponseConetent_003E5__1c;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter1d;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter1e;

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
						if (string.IsNullOrEmpty(alias))
						{
							throw new ArgumentNullException("alias");
						}
						_003Curl_003E5__1a = "https://device.jpush.cn/v3/aliases/" + alias;
						if (!string.IsNullOrEmpty(platform))
						{
							_003Curl_003E5__1a = _003Curl_003E5__1a + "?platform=" + platform;
						}
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__1a).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter1d = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00db;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter1d;
						_003C_003Eu___0024awaiter1d = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00db;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter1e;
							_003C_003Eu___0024awaiter1e = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00db:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__1b = result);
						awaiter = _003Cmsg_003E5__1b.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter1e = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseConetent_003E5__1c = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__1b.StatusCode, _003Cmsg_003E5__1b.Headers, _003CresponseConetent_003E5__1c);
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
		private struct _003CDeleteAliasAsync_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string alias;

			public string platform;

			public string _003Curl_003E5__24;

			public HttpResponseMessage _003Cmsg_003E5__25;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter26;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(alias))
						{
							throw new ArgumentNullException(alias);
						}
						_003Curl_003E5__24 = "https://device.jpush.cn/v3/aliases/" + alias;
						if (!string.IsNullOrEmpty(platform))
						{
							_003Curl_003E5__24 = _003Curl_003E5__24 + "?platform=" + platform;
						}
						awaiter = JPushClient.HttpClient.DeleteAsync(_003Curl_003E5__24).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter26 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter26;
						_003C_003Eu___0024awaiter26 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponseMessage result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
					HttpResponseMessage httpResponseMessage = _003Cmsg_003E5__25 = result;
					result2 = new HttpResponse(_003Cmsg_003E5__25.StatusCode, _003Cmsg_003E5__25.Headers, "");
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
		private struct _003CGetTagsAsync_003Ed__2b : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string _003Curl_003E5__2c;

			public HttpResponseMessage _003Cmsg_003E5__2d;

			public string _003CresponseContent_003E5__2e;

			public DeviceClient _003C_003E4__this;

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
						_003Curl_003E5__2c = "https://device.jpush.cn/v3/tags/";
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__2c).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter2f = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_008f;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter2f;
						_003C_003Eu___0024awaiter2f = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_008f;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter30;
							_003C_003Eu___0024awaiter30 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_008f:
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
		private struct _003CIsDeviceInTagAsync_003Ed__33 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string registrationId;

			public string tag;

			public string _003Curl_003E5__34;

			public HttpResponseMessage _003Cmsg_003E5__35;

			public string _003CresponseContent_003E5__36;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter37;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter38;

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
						if (string.IsNullOrEmpty(registrationId))
						{
							throw new ArgumentNullException("registrationId");
						}
						if (string.IsNullOrEmpty(tag))
						{
							throw new ArgumentNullException("tag");
						}
						_003Curl_003E5__34 = "https://device.jpush.cn/v3/tags/" + tag + "/registration_ids/" + registrationId;
						awaiter2 = JPushClient.HttpClient.GetAsync(_003Curl_003E5__34).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter37 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00d5;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter37;
						_003C_003Eu___0024awaiter37 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00d5;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter38;
							_003C_003Eu___0024awaiter38 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00d5:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__35 = result);
						awaiter = _003Cmsg_003E5__35.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter38 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseContent_003E5__36 = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__35.StatusCode, _003Cmsg_003E5__35.Headers, _003CresponseContent_003E5__36);
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
		private struct _003CAddDevicesToTagAsync_003Ed__3d : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string tag;

			public List<string> registrationIdList;

			public string _003Curl_003E5__3e;

			public JObject _003CjObj_003E5__3f;

			public StringContent _003CrequestContent_003E5__40;

			public HttpResponseMessage _003Cmsg_003E5__41;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter42;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(tag))
						{
							throw new ArgumentNullException("tag");
						}
						if (registrationIdList == null || registrationIdList.Count == 0)
						{
							throw new ArgumentException("registrationIdList");
						}
						_003Curl_003E5__3e = "https://device.jpush.cn/v3/tags/" + tag;
						_003CjObj_003E5__3f = new JObject();
						_003CjObj_003E5__3f["registration_ids"] = new JObject();
						_003CjObj_003E5__3f["registration_ids"]["add"] = new JArray(registrationIdList);
						_003CrequestContent_003E5__40 = new StringContent(_003CjObj_003E5__3f.ToString(), Encoding.UTF8);
						awaiter = JPushClient.HttpClient.PostAsync(_003Curl_003E5__3e, _003CrequestContent_003E5__40).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter42 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter42;
						_003C_003Eu___0024awaiter42 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponseMessage result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
					HttpResponseMessage httpResponseMessage = _003Cmsg_003E5__41 = result;
					result2 = new HttpResponse(_003Cmsg_003E5__41.StatusCode, _003Cmsg_003E5__41.Headers, "");
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
		private struct _003CRemoveDevicesFromTagAsync_003Ed__47 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string tag;

			public List<string> registrationIdList;

			public string _003Curl_003E5__48;

			public JObject _003CjObj_003E5__49;

			public StringContent _003CrequestContent_003E5__4a;

			public HttpResponseMessage _003Cmsg_003E5__4b;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter4c;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(tag))
						{
							throw new ArgumentNullException("tag");
						}
						if (registrationIdList == null || registrationIdList.Count == 0)
						{
							throw new ArgumentException("registrationIdList");
						}
						_003Curl_003E5__48 = "https://device.jpush.cn/v3/tags/" + tag;
						_003CjObj_003E5__49 = new JObject();
						_003CjObj_003E5__49["registration_ids"] = new JObject();
						_003CjObj_003E5__49["registration_ids"]["remove"] = new JArray(registrationIdList);
						_003CrequestContent_003E5__4a = new StringContent(_003CjObj_003E5__49.ToString(), Encoding.UTF8);
						awaiter = JPushClient.HttpClient.PostAsync(_003Curl_003E5__48, _003CrequestContent_003E5__4a).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter4c = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter4c;
						_003C_003Eu___0024awaiter4c = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponseMessage result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
					HttpResponseMessage httpResponseMessage = _003Cmsg_003E5__4b = result;
					result2 = new HttpResponse(_003Cmsg_003E5__4b.StatusCode, _003Cmsg_003E5__4b.Headers, "");
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
		private struct _003CDeleteTagAsync_003Ed__51 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public string tag;

			public string platform;

			public string _003Curl_003E5__52;

			public HttpResponseMessage _003Cmsg_003E5__53;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter54;

			private object _003C_003Et__stack;

			private void MoveNext()
			{
				HttpResponse result2;
				try
				{
					bool flag = true;
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter;
					if (_003C_003E1__state != 0)
					{
						if (string.IsNullOrEmpty(tag))
						{
							throw new ArgumentNullException("tag");
						}
						_003Curl_003E5__52 = "https://device.jpush.cn/v3/tags/" + tag;
						if (!string.IsNullOrEmpty(platform))
						{
							_003Curl_003E5__52 = _003Curl_003E5__52 + "?platform=" + platform;
						}
						awaiter = JPushClient.HttpClient.DeleteAsync(_003Curl_003E5__52).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter54 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu___0024awaiter54;
						_003C_003Eu___0024awaiter54 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
					}
					HttpResponseMessage result = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
					HttpResponseMessage httpResponseMessage = _003Cmsg_003E5__53 = result;
					result2 = new HttpResponse(_003Cmsg_003E5__53.StatusCode, _003Cmsg_003E5__53.Headers, "");
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
		private struct _003CGetUserOnlineStatusAsync_003Ed__59 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder;

			public List<string> registrationIdList;

			public string _003Curl_003E5__5a;

			public string _003CrequestJson_003E5__5b;

			public HttpContent _003CrequestContent_003E5__5c;

			public HttpResponseMessage _003Cmsg_003E5__5d;

			public string _003CresponseContent_003E5__5e;

			public DeviceClient _003C_003E4__this;

			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter5f;

			private object _003C_003Et__stack;

			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter _003C_003Eu___0024awaiter60;

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
						if (registrationIdList == null || registrationIdList.Count == 0)
						{
							throw new ArgumentException("registrationIdList");
						}
						_003Curl_003E5__5a = "https://device.jpush.cn/v3/devices/status/";
						_003CrequestJson_003E5__5b = JsonConvert.SerializeObject(registrationIdList);
						_003CrequestContent_003E5__5c = new StringContent(_003CrequestJson_003E5__5b, Encoding.UTF8);
						awaiter2 = JPushClient.HttpClient.PostAsync(_003Curl_003E5__5a, _003CrequestContent_003E5__5c).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							_003C_003E1__state = 0;
							_003C_003Eu___0024awaiter5f = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							flag = false;
							return;
						}
						goto IL_00dc;
					case 0:
						awaiter2 = _003C_003Eu___0024awaiter5f;
						_003C_003Eu___0024awaiter5f = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						_003C_003E1__state = -1;
						goto IL_00dc;
					case 1:
						{
							awaiter = _003C_003Eu___0024awaiter60;
							_003C_003Eu___0024awaiter60 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							_003C_003E1__state = -1;
							break;
						}
						IL_00dc:
						result = awaiter2.GetResult();
						awaiter2 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						httpResponseMessage = (_003Cmsg_003E5__5d = result);
						awaiter = _003Cmsg_003E5__5d.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							_003C_003E1__state = 1;
							_003C_003Eu___0024awaiter60 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							flag = false;
							return;
						}
						break;
					}
					string result2 = awaiter.GetResult();
					awaiter = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
					string text = _003CresponseContent_003E5__5e = result2;
					result3 = new HttpResponse(_003Cmsg_003E5__5d.StatusCode, _003Cmsg_003E5__5d.Headers, _003CresponseContent_003E5__5e);
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

		private const string BASE_URL = "https://device.jpush.cn";

		[AsyncStateMachine(typeof(_003CGetDeviceInfoAsync_003Ed__0))]
		[DebuggerStepThrough]
		public Task<HttpResponse> GetDeviceInfoAsync(string registrationId)
		{
			_003CGetDeviceInfoAsync_003Ed__0 stateMachine = default(_003CGetDeviceInfoAsync_003Ed__0);
			stateMachine._003C_003E4__this = this;
			stateMachine.registrationId = registrationId;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetDeviceInfo(string registrationId)
		{
			Task<HttpResponse> task = Task.Run(() => GetDeviceInfoAsync(registrationId));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CUpdateDeviceInfoAsync_003Ed__a))]
		[DebuggerStepThrough]
		public Task<HttpResponse> UpdateDeviceInfoAsync(string registrationId, string json)
		{
			_003CUpdateDeviceInfoAsync_003Ed__a stateMachine = default(_003CUpdateDeviceInfoAsync_003Ed__a);
			stateMachine._003C_003E4__this = this;
			stateMachine.registrationId = registrationId;
			stateMachine.json = json;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CUpdateDeviceInfoAsync_003Ed__12))]
		public Task<HttpResponse> UpdateDeviceInfoAsync(string registrationId, DevicePayload devicePayload)
		{
			_003CUpdateDeviceInfoAsync_003Ed__12 stateMachine = default(_003CUpdateDeviceInfoAsync_003Ed__12);
			stateMachine._003C_003E4__this = this;
			stateMachine.registrationId = registrationId;
			stateMachine.devicePayload = devicePayload;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse UpdateDeviceInfo(string registrationId, DevicePayload devicePayload)
		{
			Task<HttpResponse> task = Task.Run(() => UpdateDeviceInfoAsync(registrationId, devicePayload));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CGetDevicesByAliasAsync_003Ed__19))]
		public Task<HttpResponse> GetDevicesByAliasAsync(string alias, string platform)
		{
			_003CGetDevicesByAliasAsync_003Ed__19 stateMachine = default(_003CGetDevicesByAliasAsync_003Ed__19);
			stateMachine._003C_003E4__this = this;
			stateMachine.alias = alias;
			stateMachine.platform = platform;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetDeviceByAlias(string alias, string platform)
		{
			Task<HttpResponse> task = Task.Run(() => GetDevicesByAliasAsync(alias, platform));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CDeleteAliasAsync_003Ed__23))]
		[DebuggerStepThrough]
		public Task<HttpResponse> DeleteAliasAsync(string alias, string platform)
		{
			_003CDeleteAliasAsync_003Ed__23 stateMachine = default(_003CDeleteAliasAsync_003Ed__23);
			stateMachine._003C_003E4__this = this;
			stateMachine.alias = alias;
			stateMachine.platform = platform;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse DeleteAlias(string alias, string platform)
		{
			Task<HttpResponse> task = Task.Run(() => DeleteAliasAsync(alias, platform));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CGetTagsAsync_003Ed__2b))]
		[DebuggerStepThrough]
		public Task<HttpResponse> GetTagsAsync()
		{
			_003CGetTagsAsync_003Ed__2b stateMachine = default(_003CGetTagsAsync_003Ed__2b);
			stateMachine._003C_003E4__this = this;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetTags()
		{
			Task<HttpResponse> task = Task.Run(() => GetTagsAsync());
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CIsDeviceInTagAsync_003Ed__33))]
		public Task<HttpResponse> IsDeviceInTagAsync(string registrationId, string tag)
		{
			_003CIsDeviceInTagAsync_003Ed__33 stateMachine = default(_003CIsDeviceInTagAsync_003Ed__33);
			stateMachine._003C_003E4__this = this;
			stateMachine.registrationId = registrationId;
			stateMachine.tag = tag;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse IsDeviceInTag(string registrationId, string tag)
		{
			Task<HttpResponse> task = Task.Run(() => IsDeviceInTagAsync(registrationId, tag));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CAddDevicesToTagAsync_003Ed__3d))]
		[DebuggerStepThrough]
		public Task<HttpResponse> AddDevicesToTagAsync(string tag, List<string> registrationIdList)
		{
			_003CAddDevicesToTagAsync_003Ed__3d stateMachine = default(_003CAddDevicesToTagAsync_003Ed__3d);
			stateMachine._003C_003E4__this = this;
			stateMachine.tag = tag;
			stateMachine.registrationIdList = registrationIdList;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse AddDevicesToTag(string tag, List<string> registrationIdList)
		{
			Task<HttpResponse> task = Task.Run(() => AddDevicesToTagAsync(tag, registrationIdList));
			task.Wait();
			return task.Result;
		}

		[DebuggerStepThrough]
		[AsyncStateMachine(typeof(_003CRemoveDevicesFromTagAsync_003Ed__47))]
		public Task<HttpResponse> RemoveDevicesFromTagAsync(string tag, List<string> registrationIdList)
		{
			_003CRemoveDevicesFromTagAsync_003Ed__47 stateMachine = default(_003CRemoveDevicesFromTagAsync_003Ed__47);
			stateMachine._003C_003E4__this = this;
			stateMachine.tag = tag;
			stateMachine.registrationIdList = registrationIdList;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse RemoveDevicesFromTag(string tag, List<string> registrationIdList)
		{
			Task<HttpResponse> task = Task.Run(() => RemoveDevicesFromTagAsync(tag, registrationIdList));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CDeleteTagAsync_003Ed__51))]
		[DebuggerStepThrough]
		public Task<HttpResponse> DeleteTagAsync(string tag, string platform)
		{
			_003CDeleteTagAsync_003Ed__51 stateMachine = default(_003CDeleteTagAsync_003Ed__51);
			stateMachine._003C_003E4__this = this;
			stateMachine.tag = tag;
			stateMachine.platform = platform;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse DeleteTag(string tag, string platform)
		{
			Task<HttpResponse> task = Task.Run(() => DeleteTagAsync(tag, platform));
			task.Wait();
			return task.Result;
		}

		[AsyncStateMachine(typeof(_003CGetUserOnlineStatusAsync_003Ed__59))]
		[DebuggerStepThrough]
		public Task<HttpResponse> GetUserOnlineStatusAsync(List<string> registrationIdList)
		{
			_003CGetUserOnlineStatusAsync_003Ed__59 stateMachine = default(_003CGetUserOnlineStatusAsync_003Ed__59);
			stateMachine._003C_003E4__this = this;
			stateMachine.registrationIdList = registrationIdList;
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<HttpResponse>.Create();
			stateMachine._003C_003E1__state = -1;
			AsyncTaskMethodBuilder<HttpResponse> _003C_003Et__builder = stateMachine._003C_003Et__builder;
			_003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public HttpResponse GetUserOnlineStatus(List<string> registrationIdList)
		{
			Task<HttpResponse> task = Task.Run(() => GetUserOnlineStatusAsync(registrationIdList));
			task.Wait();
			return task.Result;
		}
	}
}
