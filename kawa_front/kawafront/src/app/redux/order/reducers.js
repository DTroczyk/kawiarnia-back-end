import types from "./types";
const INITIAL_STATE = {
  coffeeName:"",
  waterCount: 0,
  espressoCount: 0,
  milkCount: 0,
  isContainChocolate: false,
  latLng: null,
  price: 50,
};

const orderReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case types.SET_LAT_LNG: {
      return { ...state, latLng: action.payload };
    }
    case types.ADD_ESPRESSO: {
      return { ...state, espressoCount: state.espressoCount + 1 };
    }
    case types.TOGGLE_CHOCOLATE: {
      return { ...state, isContainChocolate: !state.isContainChocolate };
    }
    case types.ADD_MILK: {
      return { ...state, milkCount: state.milkCount + 1 };
    }
    case types.DELETE_MILK: {
      return { ...state, milkCount: state.milkCount - 1 };
    }
    case types.DELETE_ESPRESSO: {
      return { ...state, espressoCount: state.espressoCount - 1 };
    }
    case types.SET_PRESET_OF_COFFEE: {
      const { espressoCount, milkCount, isContainChocolate, waterCount,coffeeName } = action.payload;
      return { ...state, espressoCount, milkCount, isContainChocolate, waterCount,coffeeName };
    }
    case types.ADD_WATER: {
      return {...state, waterCount: state.waterCount + 1};
    }
    case types.DELETE_WATER: {
      return {...state, waterCount: state.waterCount - 1};
    }
    default:
      return { ...state };
  }
};
export default orderReducer;
