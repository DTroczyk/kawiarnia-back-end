import types from "./types";
const INITIAL_STATE = {
  bucketItems: [
    {
      date: "18-05-2020 19:31",
      coffeeName: "latte",
      count: 3,
      price: 20.3,
      status: "opłacono",
      paymentMethod: "blik",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      coffeeName: "americana",
      count: 3,
      price: 20.3,
      status: "opłacono",
      paymentMethod: "blik",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      coffeeName: "flat white",
      count: 3,
      price: 20.3,
      status: "opłacono",
      paymentMethod: "blik",
      isCollapsed: false,
    },
    {
      date: "18-05-2020 19:31",
      coffeeName: "latte",
      count: 3,
      price: 23,
      status: "opłacono",
      paymentMethod: "blik",
      isCollapsed: false,
    },
  ],
};

const bucketReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case types.ADD_ITEM_TO_BUCKET: {
      console.log(action.payload);
      const newData = Array.from(state.bucketItems);
      newData.push(action.payload);
      return { ...state, bucketItems: newData };
    }
    case types.DELETE_ITEM_FROM_BUCKET: {
      const newData = Array.from(state.bucketItems);
      newData.splice(action.payload, 1);

      return { ...state, bucketItems: newData };
    }
    default:
      return { ...state };
  }
};
export default bucketReducer;
