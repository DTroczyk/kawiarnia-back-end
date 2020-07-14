import types from "./types";
const INITIAL_STATE = [
  [
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      isCollapsed: true,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      isCollapsed: false,
    },
  ],
];

const historyReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case types.TOGGLE_ORDER_DETAILS_VISIBLE: {
      const { idx } = action.payload;
      let newData = Object.assign({}, state[0]);
      console.log(newData);
      return { ...state };
    }
    default:
      return { ...state };
  }
};
export default historyReducer;
