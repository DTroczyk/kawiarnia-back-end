import types from "./types";

const addItemToBucket = (payload) => ({
  type: types.ADD_ITEM_TO_BUCKET,
  payload,
});
const deleteItemFromBucket = (payload) => ({
  type: types.DELETE_ITEM_FROM_BUCKET,
  payload,
});
export default {
  addItemToBucket,
  deleteItemFromBucket,
};
