import types from "./types";

const toggleOrderDetailsVisible = (payload) => ({
  type: types.TOGGLE_ORDER_DETAILS_VISIBLE,
  payload,
});

export default {
  toggleOrderDetailsVisible,
};
