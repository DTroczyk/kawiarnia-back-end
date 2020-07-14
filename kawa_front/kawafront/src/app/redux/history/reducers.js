import types from "./types";
const INITIAL_STATE = {
  historyItems: [
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      paymentMethod:"blik",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      paymentMethod:"visa",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      paymentMethod:"visa",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      paymentMethod:"visa",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      name: "latte",
      count: 3,
      price: "20.30 zł",
      status: "opłacono",
      paymentMethod:"visa",
      isCollapsed: false,
    },
  ],
};

const historyReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case types.TOGGLE_ORDER_DETAILS_VISIBLE: {
      const  idx  = action.payload;
      console.log(idx)
      let newData = Array.from(state.historyItems);
      newData[idx].isCollapsed=!newData[idx].isCollapsed

      return { ...state,historyItems:newData };
    }
    default:
      return { ...state };
  }
};
export default historyReducer;
