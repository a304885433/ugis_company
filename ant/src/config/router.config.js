// eslint-disable-next-line
import { UserLayout, BasicLayout, RouteView, BlankLayout, PageView } from '@/layouts'
import { bxAnaalyse } from '@/core/icons'

export const asyncRouterMap = [

  {
    path: '/',
    name: 'index',
    component: BasicLayout,
    meta: { title: '首页' },
    redirect: '/manage/check/list',
    children: [
      // dashboard
      {
        path: '/dashboard',
        name: 'dashboard',
        // redirect: '/manage/check/list',
        // component: RouteView,
        component: () => import('@/views/Html'),
        meta: { title: '首页',  html: 'html2', icon: bxAnaalyse },
      },
      //   children: [
      //     {
      //       path: '/dashboard/analysis',
      //       name: 'Analysis',
      //       component: () => import('@/views/dashboard/Analysis'),
      //       meta: { title: '分析页', keepAlive: false, permission: ['dashboard'] }
      //     },
      //     // 外部链接
      //     {
      //       path: 'https://www.baidu.com/',
      //       name: 'Monitor',
      //       meta: { title: '监控页（外部）', target: '_blank' }
      //     },
      //     {
      //       path: '/dashboard/workplace',
      //       name: 'Workplace',
      //       component: () => import('@/views/dashboard/Workplace'),
      //       meta: { title: '工作台', permission: ['dashboard'] }
      //     }
      //   ]
      // },

      // // forms
      // {
      //   path: '/form',
      //   redirect: '/form/base-form',
      //   component: PageView,
      //   meta: { title: '表单页', icon: 'form', permission: ['form'] },
      //   children: [
      //     {
      //       path: '/form/base-form',
      //       name: 'BaseForm',
      //       component: () => import('@/views/form/BasicForm'),
      //       meta: { title: '基础表单', permission: ['form'] }
      //     },
      //     {
      //       path: '/form/step-form',
      //       name: 'StepForm',
      //       component: () => import('@/views/form/stepForm/StepForm'),
      //       meta: { title: '分步表单', permission: ['form'] }
      //     },
      //     {
      //       path: '/form/advanced-form',
      //       name: 'AdvanceForm',
      //       component: () => import('@/views/form/advancedForm/AdvancedForm'),
      //       meta: { title: '高级表单', permission: ['form'] }
      //     }
      //   ]
      // },

      // // list
      // {
      //   path: '/list',
      //   name: 'list',
      //   component: PageView,
      //   redirect: '/list/table-list',
      //   meta: { title: '列表页', icon: 'table', permission: ['table'] },
      //   children: [
      //     {
      //       path: '/list/table-list',
      //       name: 'TableListWrapper',
      //       hideChildrenInMenu: true, // 强制显示 MenuItem 而不是 SubMenu
      //       component: () => import('@/views/list/TableList'),
      //       meta: { title: '查询表格', permission: ['table'] }
      //     },
      //     {
      //       path: '/list/basic-list',
      //       name: 'BasicList',
      //       component: () => import('@/views/list/StandardList'),
      //       meta: { title: '标准列表', permission: ['table'] }
      //     },
      //     {
      //       path: '/list/card',
      //       name: 'CardList',
      //       component: () => import('@/views/list/CardList'),
      //       meta: { title: '卡片列表', permission: ['table'] }
      //     },
      //     {
      //       path: '/list/search',
      //       name: 'SearchList',
      //       component: () => import('@/views/list/search/SearchLayout'),
      //       redirect: '/list/search/article',
      //       meta: { title: '搜索列表', permission: ['table'] },
      //       children: [
      //         {
      //           path: '/list/search/article',
      //           name: 'SearchArticles',
      //           component: () => import('../views/list/search/Article'),
      //           meta: { title: '搜索列表（文章）', permission: ['table'] }
      //         },
      //         {
      //           path: '/list/search/project',
      //           name: 'SearchProjects',
      //           component: () => import('../views/list/search/Projects'),
      //           meta: { title: '搜索列表（项目）', permission: ['table'] }
      //         },
      //         {
      //           path: '/list/search/application',
      //           name: 'SearchApplications',
      //           component: () => import('../views/list/search/Applications'),
      //           meta: { title: '搜索列表（应用）', permission: ['table'] }
      //         }
      //       ]
      //     }
      //   ]
      // },

      // // profile
      // {
      //   path: '/profile',
      //   name: 'profile',
      //   component: RouteView,
      //   redirect: '/profile/basic',
      //   meta: { title: '详情页', icon: 'profile', permission: ['profile'] },
      //   children: [
      //     {
      //       path: '/profile/basic',
      //       name: 'ProfileBasic',
      //       component: () => import('@/views/profile/basic/Index'),
      //       meta: { title: '基础详情页', permission: ['profile'] }
      //     },
      //     {
      //       path: '/profile/advanced',
      //       name: 'ProfileAdvanced',
      //       component: () => import('@/views/profile/advanced/Advanced'),
      //       meta: { title: '高级详情页', permission: ['profile'] }
      //     }
      //   ]
      // },

      // // result
      // {
      //   path: '/result',
      //   name: 'result',
      //   component: PageView,
      //   redirect: '/result/success',
      //   meta: { title: '结果页', icon: 'check-circle-o', permission: ['result'] },
      //   children: [
      //     {
      //       path: '/result/success',
      //       name: 'ResultSuccess',
      //       component: () => import(/* webpackChunkName: "result" */ '@/views/result/Success'),
      //       meta: { title: '成功', keepAlive: false, hiddenHeaderContent: true, permission: ['result'] }
      //     },
      //     {
      //       path: '/result/fail',
      //       name: 'ResultFail',
      //       component: () => import(/* webpackChunkName: "result" */ '@/views/result/Error'),
      //       meta: { title: '失败', keepAlive: false, hiddenHeaderContent: true, permission: ['result'] }
      //     }
      //   ]
      // },

      // // Exception
      // {
      //   path: '/exception',
      //   name: 'exception',
      //   component: RouteView,
      //   redirect: '/exception/403',
      //   meta: { title: '异常页', icon: 'warning', permission: ['exception'] },
      //   children: [
      //     {
      //       path: '/exception/403',
      //       name: 'Exception403',
      //       component: () => import(/* webpackChunkName: "fail" */ '@/views/exception/403'),
      //       meta: { title: '403', permission: ['exception'] }
      //     },
      //     {
      //       path: '/exception/404',
      //       name: 'Exception404',
      //       component: () => import(/* webpackChunkName: "fail" */ '@/views/exception/404'),
      //       meta: { title: '404', permission: ['exception'] }
      //     },
      //     {
      //       path: '/exception/500',
      //       name: 'Exception500',
      //       component: () => import(/* webpackChunkName: "fail" */ '@/views/exception/500'),
      //       meta: { title: '500', permission: ['exception'] }
      //     }
      //   ]
      // },

      // // account
      // {
      //   path: '/account',
      //   component: RouteView,
      //   redirect: '/account/center',
      //   name: 'account',
      //   meta: { title: '个人页', icon: 'user', permission: ['user'] },
      //   children: [
      //     {
      //       path: '/account/center',
      //       name: 'center',
      //       component: () => import('@/views/account/center/Index'),
      //       meta: { title: '个人中心', permission: ['user'] }
      //     },
      //     {
      //       path: '/account/settings',
      //       name: 'settings',
      //       component: () => import('@/views/account/settings/Index'),
      //       meta: { title: '个人设置', hideHeader: true, permission: ['user'] },
      //       redirect: '/account/settings/base',
      //       hideChildrenInMenu: true,
      //       children: [
      //         {
      //           path: '/account/settings/base',
      //           name: 'BaseSettings',
      //           component: () => import('@/views/account/settings/BaseSetting'),
      //           meta: { title: '基本设置', hidden: true, permission: ['user'] }
      //         },
      //         {
      //           path: '/account/settings/security',
      //           name: 'SecuritySettings',
      //           component: () => import('@/views/account/settings/Security'),
      //           meta: { title: '安全设置', hidden: true, permission: ['user'] }
      //         },
      //         {
      //           path: '/account/settings/custom',
      //           name: 'CustomSettings',
      //           component: () => import('@/views/account/settings/Custom'),
      //           meta: { title: '个性化设置', hidden: true, permission: ['user'] }
      //         },
      //         {
      //           path: '/account/settings/binding',
      //           name: 'BindingSettings',
      //           component: () => import('@/views/account/settings/Binding'),
      //           meta: { title: '账户绑定', hidden: true, permission: ['user'] }
      //         },
      //         {
      //           path: '/account/settings/notification',
      //           name: 'NotificationSettings',
      //           component: () => import('@/views/account/settings/Notification'),
      //           meta: { title: '新消息通知', hidden: true, permission: ['user'] }
      //         }
      //       ]
      //     }
      //   ]
      // },

      // other
      // {
      //   path: '/other',
      //   name: 'otherPage',
      //   component: PageView,
      //   meta: { title: '其他组件', icon: 'slack', permission: ['dashboard'] },
      //   redirect: '/other/icon-selector',
      //   children: [
      //     {
      //       path: '/other/icon-selector',
      //       name: 'TestIconSelect',
      //       component: () => import('@/views/other/IconSelectorView'),
      //       meta: { title: 'IconSelector', icon: 'tool', permission: ['dashboard'] }
      //     },
      //     {
      //       path: '/other/list',
      //       component: RouteView,
      //       meta: { title: '业务布局', icon: 'layout', permission: ['support'] },
      //       redirect: '/other/list/tree-list',
      //       children: [
      //         {
      //           path: '/other/list/tree-list',
      //           name: 'TreeList',
      //           component: () => import('@/views/other/TreeList'),
      //           meta: { title: '树目录表格' }
      //         },
      //         {
      //           path: '/other/list/edit-table',
      //           name: 'EditList',
      //           component: () => import('@/views/other/TableInnerEditList'),
      //           meta: { title: '内联编辑表格' }
      //         },
      //         {
      //           path: '/other/list/user-list',
      //           name: 'UserList',
      //           component: () => import('@/views/other/UserList'),
      //           meta: { title: '用户列表' }
      //         },
      //         {
      //           path: '/other/list/role-list',
      //           name: 'RoleList',
      //           component: () => import('@/views/other/RoleList'),
      //           meta: { title: '角色列表' }
      //         },
      //         {
      //           path: '/other/list/system-role',
      //           name: 'SystemRole',
      //           component: () => import('@/views/role/RoleList'),
      //           meta: { title: '角色列表2' }
      //         },
      //         {
      //           path: '/other/list/permission-list',
      //           name: 'PermissionList',
      //           component: () => import('@/views/other/PermissionList'),
      //           meta: { title: '权限列表' }
      //         }
      //       ]
      //     }
      //   ]
      // },
      // 管理数据
      {
        path: '/manage',
        name: 'manage',
        component: PageView,
        meta: { title: '信息录入', icon: 'slack' },
        children: [
          {
            path: '/manage/company',
            name: 'CompanyListIndex',
            redirect: '/manage/company/list',
            component: () => import('@/views/RouteView'),
            hideChildrenInMenu: true,
            meta: { title: '企业信息', permission: ['CompanyManager'] },
            children: [
              {
                path: '/manage/company/list',
                name: 'CompanyList',
                component: () => import('@/views/func1/company/CompanyList'),
                meta: {
                  title: '企业信息', hidden: true, permission: ['CompanyManager']
                },
              },
              {
                path: '/manage/company/edit/',
                name: 'CompanyEdit',
                hidden: true,
                component: () => import('@/views/func1/company/companyEdit/Index'),
                meta: {
                  title: '新增企业', hidden: true, permission: ['CompanyManager']
                }
              },
            ]
          },
          {
            path: '/manage/check/list',
            name: 'CheckIndex',
            hideChildrenInMenu: true,
            redirect: '/manage/check/list',
            component: () => import('@/views/RouteView.vue'),
            meta: { title: '检测数据', },
            children: [
              {
                path: '/manage/check/list',
                name: 'CheckList',
                component: () => import('@/views/func1/chkResult/CheckList'),
                meta: { title: '', hidden: true, permission: ['CheckManager'] }
              },
              // {
              //   path: '/manage/check/wizard',
              //   name: 'CheckWizard',
              //   component: () => import('@/views/func1/chkResult/CheckWizard/Index'),
              //   meta: { title: '增加排查', hidden: true, permission: ['user'] }
              // },
              {
                path: '/manage/check/edit',
                name: 'CheckEdit',
                component: () => import('@/views/func1/chkResult/CheckEdit/Index'),
                meta: { title: '新增检测数据', hidden: true, permission: ['CheckManager'] }
              },
            ]
          },
        ]
      },
      // 系统数据
      {
        path: '/report',
        name: 'Report',
        component: PageView,
        meta: { title: '数据调用', icon: 'slack', },
        redirect: '/report/report1',
        children: [
          {
            path: '/report/report1',
            name: 'Report1',
            component: () => import('@/views/func1/report/Report1'),
            // component: () => import('@/views/other/RoleList'),
            meta: { title: '企业排查统计', permission: ['Report1'] }
          },
          {
            path: '/report/report2',
            name: 'Report2',
            component: () => import('@/views/func1/report/Report2'),
            // component: () => import('@/views/other/RoleList'),
            meta: { title: '企业因子统计', permission: ['Report2'] }
          },
          {
            path: '/report/report3',
            name: 'Report3',
            component: () => import('@/views/func1/report/Report3'),
            // component: () => import('@/views/other/RoleList'),
            meta: { title: '所有因子统计', permission: ['Report3'] }
          },
          {
            path: '/report/report4',
            name: 'Report4',
            component: () => import('@/views/func1/report/Report4'),
            // component: () => import('@/views/other/RoleList'),
            meta: { title: '企业点位统计', permission: ['Report4'] }
          },
          {
            path: '/report/report5',
            name: 'Report5',
            component: () => import('@/views/func1/report/Report5'),
            // component: () => import('@/views/other/RoleList'),
            meta: { title: '所有点位统计', permission: ['Report5'] }
          },
          {
            path: '/report/report6',
            name: 'Report6',
            component: () => import('@/views/func1/report/Report6'),
            // component: () => import('@/views/other/RoleList'),
            meta: { title: '检测数据查询', permission: ['CheckManager'] }
          }
        ]
      },
      // 系统数据
      {
        path: '/system',
        name: 'system',
        component: PageView,
        meta: { title: '系统设置', icon: 'slack' },
        redirect: '/system/dict-list',
        children: [
          {
            path: '/system/dict-list',
            name: 'DictList',
            component: () => import('@/views/func1/dic/DictList'),
            meta: { title: '基础数据', permission: ['BaseDataManager'] }
          },
          {
            path: '/system/role/index',
            name: 'RoleListIndex',
            redirect: '/system/role/list',
            component: () => import('@/views/RouteView'),
            meta: { title: '角色列表',  permission: ['RoleManager'] },
            hideChildrenInMenu: true,
            children: [
              {
                path: '/system/role/list',
                name: 'RoleList',
                component: () => import('@/views/func1/role/RoleList'),
                meta: { title: '角色列表', hidden: true, bread: false, permission: ['RoleManager'] }
              },
              {
                path: '/system/role/edit',
                name: 'RoleEdit',
                component: () => import('@/views/func1/role/RoleEdit'),
                meta: { title: '角色编辑', hidden: true, permission: ['RoleManager'] }
              },
            ]
          },
          {
            path: '/system/user/index',
            name: 'UserListIndex',
            redirect: '/system/user/list',
            component: () => import('@/views/RouteView'),
            meta: { title: '用户列表', },
            hideChildrenInMenu: true,
            children: [
              {
                path: '/system/user/list',
                name: 'UserList',
                component: () => import('@/views/func1/user/UserList'),
                meta: { title: '用户列表', hidden: true, bread: false, permission: ['UserManager'] }
              },
              {
                path: '/system/user/edit',
                name: 'UserEdit',
                component: () => import('@/views/func1/user/UserEdit'),
                meta: { title: '用户编辑', hidden: true, permission: ['UserManager']  }
              },
            ]
          },
          {
            path: '/system/user/password',
            name: 'ChangePassword',
            component: () => import('@/views/func1/user/ChangePassword'),
            meta: { title: '修改密码' }
          },
          {
            path: '/system/user/html',
            name: 'Html',
            component: () => import('@/views/Html'),
            meta: { title: '联系我们' , html: 'html1'  }
          },
          // {
          //   path: '/system/user/html2',
          //   name: 'Html2',
          //   component: () => import('@/views/Html'),
          //   meta: { title: '自定义' , permission: ['UserManager'], html: 'html2'  }
          // }
        ]
      }
    ]
  },
  {
    path: '*', redirect: '/404', hidden: true
  }
]

/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap = [
  {
    path: '/user',
    component: UserLayout,
    redirect: '/user/login',
    hidden: true,
    children: [
      {
        path: 'login',
        name: 'login',
        component: () => import(/* webpackChunkName: "user" */ '@/views/user/Login')
      },
      {
        path: 'register',
        name: 'register',
        component: () => import(/* webpackChunkName: "user" */ '@/views/user/Register')
      },
      {
        path: 'register-result',
        name: 'registerResult',
        component: () => import(/* webpackChunkName: "user" */ '@/views/user/RegisterResult')
      }
    ]
  },

  {
    path: '/test',
    component: BlankLayout,
    redirect: '/test/home',
    children: [
      {
        path: 'home',
        name: 'TestHome',
        component: () => import('@/views/Home')
      }
    ]
  },

  {
    path: '/404',
    component: () => import(/* webpackChunkName: "fail" */ '@/views/exception/404')
  }

]
