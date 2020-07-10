import types from "./types";

const addEspresso = () => ({ type: types.ADD_ESPRESSO });
const addMilk = () => ({ type: types.ADD_MILK });
const deleteEspresso = () => ({ type: types.DELETE_ESPRESSO });
const deleteMilk = () => ({ type: types.DELETE_MILK });
const toggleChocolate = () => ({ type: types.TOGGLE_CHOCOLATE });
const setPresetOfCoffee = (payload) => ({
  type: types.SET_PRESET_OF_COFFEE,
  payload,
});
const setLatLng=(payload)=>({type:types.SET_LAT_LNG,payload})
export default {
  addEspresso,
  addMilk,
  deleteEspresso,
  deleteMilk,
  toggleChocolate,
  setPresetOfCoffee,
  setLatLng
};
