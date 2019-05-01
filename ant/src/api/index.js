import crud from './crud';
import * as companyInfoService from './companyInfo'
import * as chkResultService from './chkResult'
const api = {
  Login: '/auth/login',
  Logout: '/auth/logout',
  ForgePassword: '/auth/forge-password',
  Register: '/auth/register',
  twoStepCode: '/auth/2step-code',
  SendSms: '/account/sms',
  SendSmsErr: '/account/sms_err',
  // get my info
  UserInfo: '/user/info',
}
export default api

export const CompanyInfo = new crud('CompanyInfo', companyInfoService)
export const DicType = new crud('DicType')
export const Dic = new crud('Dic')
export const ChkResult = new crud('ChkResult', chkResultService)
