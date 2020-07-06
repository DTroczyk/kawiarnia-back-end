import types from './types'

const loginUser=payload=>({type:types.LOGIN_USER,payload})
const logoutUser = ()=>({type:types.LOGOUT_USER})
const changeInputValue = payload =>({type:types.CHANGE_INPUT_VALUE,payload})
export default {
    loginUser,
    logoutUser,
    changeInputValue
}