import types from "./types";
const INITIAL_STATE = {
  token: null,
  data: {
    username: "anc",
    password: "ss",
    email: "aaa",
    fullName:"Jan Nowak",
    telephone:123321123,
    
  },
};

const userReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case types.LOGIN_USER: {
      return { ...state, token: "1234" };
    }
    case types.LOGOUT_USER: {
      return { ...state, token: null };
    }
    case types.CHANGE_INPUT_VALUE: {
      const { name, value } = action.payload;

      switch (name) {
        case "username": {
          const newData = Object.assign({}, state.data);
          newData.username = value;
          return { ...state, data: newData };
        }
        case "email": {
          const newData = Object.assign({}, state.data);
          newData.email = value;
          return { ...state, data: newData };
        }
        case "password": {
          const newData = Object.assign({}, state.data);
          newData.password = value;
          return { ...state, data: newData };
        }
        default:
          break;
      }
    }
    default: {
      return { ...state };
    }
  }
};

export default userReducer;
