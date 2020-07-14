import types from "./types";
const INITIAL_STATE = {
  token: null,
  data: {
    username: "anc",
    password: "ss",
    email: "aaa",
    firstName: "Jan",
    lastName: "Kowalski",
    dateOfBirth: "1997-05-16",
    road: "Wolności",
    houseNumber: "50b",
    zipcode: "42-286",
    place: "Koszęcin",
    telephone: 123321123,
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
        case "telephone": {
          const newData = Object.assign({}, state.data);
          newData.telephone = value;
          return { ...state, data: newData };
        }
        case "firstName": {
          const newData = Object.assign({}, state.data);
          newData.firstName = value;
          return { ...state, data: newData };
        }
        case "lastName": {
          const newData = Object.assign({}, state.data);
          newData.lastName = value;
          return { ...state, data: newData };
        }
        case "houseNumber": {
          const newData = Object.assign({}, state.data);
          newData.houseNumber = value;
          return { ...state, data: newData };
        }
        case "dateOfBirth": {
          const newData = Object.assign({}, state.data);
          newData.dateOfBirth = value;
          return { ...state, data: newData };
        }
        case "road": {
          const newData = Object.assign({}, state.data);
          newData.road = value;
          return { ...state, data: newData };
        }
        case "zipcode": {
          const newData = Object.assign({}, state.data);
          newData.zipcode = value;
          return { ...state, data: newData };
        }
        case "place": {
          const newData = Object.assign({}, state.data);
          newData.place = value;
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
