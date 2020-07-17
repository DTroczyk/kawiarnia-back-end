import types from "./types";

const addItemToBucket = (payload) => ({
  type: types.ADD_ITEM_TO_BUCKET,
  payload,
});
const deleteItemFromBucket = (payload) => ({
  type: types.DELETE_ITEM_FROM_BUCKET,
  payload,
});
const setElementToDelete = (payload) => ({
  type: types.SET_ELEMENT_TO_DELETE,
  payload,
});
const toggleElementToPay = (payload) => ({
  type: types.TOGGLE_ELEMENT_TO_PAY,
  payload,
});
const saveFetchedBucket = (payload) => ({
  type: types.SAVE_FETCHED_BUCKET,
  payload,
});
export default {
  addItemToBucket,
  deleteItemFromBucket,
  setElementToDelete,
  toggleElementToPay,
  saveFetchedBucket,
};
