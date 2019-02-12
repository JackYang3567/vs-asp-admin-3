using Admin.Filters;
using Admin.Models;
using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class BackController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetBaseRoleList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY RoleID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet roleList = FacadeManage.aidePlatformManagerFacade.GetRoleList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (roleList != null && roleList.PageSet != null && roleList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in roleList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						RoleID = TypeUtil.ObjectToInt(row["RoleID"]),
						RoleName = TypeUtil.ObjectToString(row["RoleName"]),
						Description = TypeUtil.ObjectToString(row["Description"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((roleList.PageSet != null && roleList.PageSet.Tables != null && roleList.PageSet.Tables[0].Rows.Count != 0) ? roleList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoBaseRoleList()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(8L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				string sqlQuery = "WHERE RoleID in (" + text + ")";
				try
				{
					FacadeManage.aidePlatformManagerFacade.DeleteRole(sqlQuery);
					return Json(new
					{
						IsOk = true,
						Msg = "删除成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "删除失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有选择操作项"
			});
		}

		[CheckCustomer]
		public ActionResult BaseRoleInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			base.ViewBag.OP = ((a == "add") ? "新增" : "编辑");
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			Base_Roles value = new Base_Roles();
			if (num > 0)
			{
				value = FacadeManage.aidePlatformManagerFacade.GetRoleInfo(num);
			}
			base.ViewData["data"] = value;
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoBaseRoleInfo(Base_Roles role)
		{
			if (role == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数不嫩为空"
				});
			}
			if (string.IsNullOrEmpty(role.RoleName))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "角色名称不能为空"
				});
			}
			Message message = new Message();
			if (role.RoleID > 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformManagerFacade.UpdateRole(role);
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(2L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				if (string.IsNullOrEmpty(role.Description))
				{
					role.Description = "";
				}
				message = FacadeManage.aidePlatformManagerFacade.InsertRole(role);
			}
			if (message.Success)
			{
				if (role.RoleID > 0)
				{
					return Json(new
					{
						IsOk = true,
						Msg = "角色信息修改成功"
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "角色信息增加成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult BaseRolePermission()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewData["data"] = null;
			base.ViewBag.RoleId = num;
			List<Base_ModuleInfo> list = new List<Base_ModuleInfo>();
			if (num > 0)
			{
				base.ViewBag.RoleName = TypeUtil.GetRoleName(num);
				if (num == 1)
				{
					base.ViewBag.Tit = "<超级管理员默认具有全部权限>";
				}
				else
				{
					DataSet moduleList = FacadeManage.aidePlatformManagerFacade.GetModuleList();
					DataSet modulePermissionList = FacadeManage.aidePlatformManagerFacade.GetModulePermissionList();
					DataSet rolePermissionList = FacadeManage.aidePlatformManagerFacade.GetRolePermissionList(num);
					if (moduleList != null && moduleList.Tables.Count > 0)
					{
						foreach (DataRow row in moduleList.Tables[0].Rows)
						{
							Base_ModuleInfo base_ModuleInfo = new Base_ModuleInfo();
							int num2 = TypeUtil.ObjectToInt(row["ModuleID"]);
							string text2 = base_ModuleInfo.Title = TypeUtil.ObjectToString(row["Title"]);
							base_ModuleInfo.ModuleID = num2;
							DataRow[] array = moduleList.Tables[0].Select("ParentID=" + num2);
							if (array != null && array.Count() > 0)
							{
								List<Base_ModuleSubInfo> list2 = new List<Base_ModuleSubInfo>();
								DataRow[] array2 = array;
								foreach (DataRow dataRow2 in array2)
								{
									Base_ModuleSubInfo base_ModuleSubInfo = new Base_ModuleSubInfo();
									string title = TypeUtil.ObjectToString(dataRow2["Title"]);
									int num3 = TypeUtil.ObjectToInt(dataRow2["ModuleID"]);
									if (modulePermissionList != null && modulePermissionList.Tables.Count > 0 && modulePermissionList.Tables[0].Select("ModuleID=" + num3).Count() > 0)
									{
										base_ModuleSubInfo.Title = title;
										base_ModuleSubInfo.ModuleID = num3;
										DataRow[] array3 = modulePermissionList.Tables[0].Select("ModuleID=" + num3);
										List<Base_ModulePermissionInfo> list3 = new List<Base_ModulePermissionInfo>();
										DataRow[] array4 = array3;
										foreach (DataRow dataRow3 in array4)
										{
											Base_ModulePermissionInfo base_ModulePermissionInfo = new Base_ModulePermissionInfo();
											int num4 = TypeUtil.ObjectToInt(dataRow3["PermissionValue"]);
											base_ModulePermissionInfo.PermissionTitle = TypeUtil.ObjectToString(dataRow3["PermissionTitle"]);
											base_ModulePermissionInfo.PermissionValue = num4;
											if (rolePermissionList != null && rolePermissionList.Tables.Count > 0 && rolePermissionList.Tables[0].Rows.Count > 0)
											{
												DataRow[] array5 = rolePermissionList.Tables[0].Select("RoleID=" + num + " and ModuleID=" + num3);
												int num5 = 0;
												if (array5 != null && array5.Count() > 0)
												{
													num5 = TypeUtil.ObjectToInt(array5[0]["OperationPermission"]);
												}
												base_ModulePermissionInfo.IsChecked = (num5 != 0 && TypeUtil.IsExit(num5, num4));
											}
											else
											{
												base_ModulePermissionInfo.IsChecked = false;
											}
											list3.Add(base_ModulePermissionInfo);
										}
										base_ModuleSubInfo.Base_ModulePermissionInfoes = list3;
									}
									if (base_ModuleSubInfo != null)
									{
										list2.Add(base_ModuleSubInfo);
									}
								}
								if (list2 != null && list2.Count > 0)
								{
									base_ModuleInfo.Base_ModuleSubInfoes = list2;
								}
							}
							if (base_ModuleInfo != null && base_ModuleInfo.Base_ModuleSubInfoes != null && base_ModuleInfo.Base_ModuleSubInfoes.Count > 0)
							{
								list.Add(base_ModuleInfo);
							}
						}
					}
				}
			}
			if (list != null && list.Count > 0)
			{
				foreach (Base_ModuleInfo item in list)
				{
					foreach (Base_ModuleSubInfo base_ModuleSubInfo2 in item.Base_ModuleSubInfoes)
					{
						if (base_ModuleSubInfo2.Base_ModulePermissionInfoes != null && base_ModuleSubInfo2.Base_ModulePermissionInfoes.Count > 0)
						{
							base_ModuleSubInfo2.Base_ModulePermissionInfoes = (from p in base_ModuleSubInfo2.Base_ModulePermissionInfoes
							where p.PermissionTitle != ""
							select p).ToList();
						}
					}
				}
				foreach (Base_ModuleInfo item2 in list)
				{
					if (item2.Base_ModuleSubInfoes != null && item2.Base_ModuleSubInfoes.Count > 0)
					{
						item2.Base_ModuleSubInfoes = item2.Base_ModuleSubInfoes.Where(delegate(Base_ModuleSubInfo p)
						{
							if (p.Title != "")
							{
								return p.Base_ModulePermissionInfoes != null;
							}
							return false;
						}).ToList();
					}
				}
				if (list != null && list.Count > 0)
				{
					list = list.Where(delegate(Base_ModuleInfo p)
					{
						if (p.Title != "" && p.Base_ModuleSubInfoes != null)
						{
							return p.Base_ModuleSubInfoes.Count > 0;
						}
						return false;
					}).ToList();
				}
			}
			base.ViewData["data"] = list;
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoBaseRolePermission()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			string value = TypeUtil.ObjectToString(base.Request["list"]);
			List<Base_RolePermission> list = null;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<List<Base_RolePermission>>(value);
			}
			if (list == null || list.Count < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			int roleID = TypeUtil.ObjectToInt(base.Request["RoleID"]);
			FacadeManage.aidePlatformManagerFacade.DeleteRolePermission(roleID);
			int num = 0;
			int num2 = 0;
			foreach (Base_RolePermission item in list)
			{
				Message message = FacadeManage.aidePlatformManagerFacade.InsertRolePermission(item);
				if (message.Success)
				{
					num2++;
				}
				else
				{
					num++;
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "总共更新" + list.Count + "条，成功：" + num2 + "条，失败：" + num + "条"
			});
		}

		[CheckCustomer]
		public ActionResult BaseUserList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetBaseUserList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY UserID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet userList = FacadeManage.aidePlatformManagerFacade.GetUserList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (userList != null && userList.PageSet != null && userList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in userList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						UserID = TypeUtil.ObjectToInt(row["UserID"]),
						RoleID = TypeUtil.ObjectToInt(row["RoleID"]),
						UserName = TypeUtil.ObjectToString(row["UserName"]),
						RoleName = TypeUtil.GetRoleName(TypeUtil.ObjectToInt(row["RoleID"])),
						NullityStatus = TypeUtil.GetNullityStatus((byte)TypeUtil.ObjectToInt(row["Nullity"])),
						PreLoginIP = TypeUtil.ObjectToString(row["PreLoginIP"]),
						PreLogintime = TypeUtil.ObjectToString(row["PreLogintime"]),
						LastLoginIP = TypeUtil.ObjectToString(row["LastLoginIP"]),
						LastLoginTime = TypeUtil.ObjectToString(row["LastLoginTime"]),
						LoginTimes = TypeUtil.ObjectToString(row["LoginTimes"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((userList.PageSet != null && userList.PageSet.Tables != null && userList.PageSet.Tables[0].Rows.Count != 0) ? userList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoBaseUserList()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			if (!string.IsNullOrEmpty(text))
			{
				switch (num)
				{
				default:
					return Json(new
					{
						IsOk = false,
						Msg = "请选择操作"
					});
				case 1:
					if (user != null)
					{
						AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
						if (!adminPermission2.GetPermission(4L))
						{
							return Json(new
							{
								IsOk = false,
								Msg = "没有权限",
								Url = "/NoPower/Index"
							});
						}
					}
					try
					{
						FacadeManage.aidePlatformManagerFacade.ModifyUserNullity(text, true);
						return Json(new
						{
							IsOk = true,
							Msg = "冻结成功"
						});
					}
					catch
					{
						return Json(new
						{
							IsOk = false,
							Msg = "冻结失败"
						});
					}
				case 2:
					if (user != null)
					{
						AdminPermission adminPermission3 = new AdminPermission(user, user.MoudleID);
						if (!adminPermission3.GetPermission(4L))
						{
							return Json(new
							{
								IsOk = false,
								Msg = "没有权限",
								Url = "/NoPower/Index"
							});
						}
					}
					try
					{
						FacadeManage.aidePlatformManagerFacade.ModifyUserNullity(text, false);
						return Json(new
						{
							IsOk = true,
							Msg = "解冻成功"
						});
					}
					catch
					{
						return Json(new
						{
							IsOk = false,
							Msg = "解冻失败"
						});
					}
				case 3:
				{
					if (user != null)
					{
						AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
						if (!adminPermission.GetPermission(8L))
						{
							return Json(new
							{
								IsOk = false,
								Msg = "没有权限",
								Url = "/NoPower/Index"
							});
						}
					}
					string userIDList = "WHERE UserID in (" + text + ")";
					try
					{
						FacadeManage.aidePlatformManagerFacade.DeleteUser(userIDList);
						return Json(new
						{
							IsOk = true,
							Msg = "删除成功"
						});
					}
					catch
					{
						return Json(new
						{
							IsOk = false,
							Msg = "删除失败"
						});
					}
				}
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有选择操作项"
			});
			IL_0202:
			return Json(new
			{
				IsOk = false,
				Msg = "没有进行任何操作"
			});
		}

		[CheckCustomer]
		public ActionResult BaseUserInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["op"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewData["data"] = null;
			if (a == "add")
			{
				base.ViewBag.OP = "新增";
			}
			else
			{
				base.ViewBag.OP = "编辑";
			}
			if (num > 0)
			{
				Base_Users userByUserID = FacadeManage.aidePlatformManagerFacade.GetUserByUserID(num);
				base.ViewData["data"] = userByUserID;
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoBaseUserInfo(Base_Users entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交数据"
				});
			}
			if (string.IsNullOrEmpty(entity.Password))
			{
				entity.Password = entity.BandIP;
			}
			else
			{
				entity.Password = Utility.MD5(entity.Password);
			}
			Message message = new Message();
			if (entity.UserID > 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformManagerFacade.ModifyUserInfo(entity);
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(2L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				entity.LastLoginIP = Utility.UserIP;
				entity.IsBand = 1;
				message = FacadeManage.aidePlatformManagerFacade.Register(entity);
			}
			if (message.Success)
			{
				if (entity.UserID < 1)
				{
					return Json(new
					{
						IsOk = true,
						Msg = "用户信息增加成功"
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "用户信息修改成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		public ActionResult BaseUserUpdate()
		{
			return View();
		}
	}
}
