import types from "./types";
const INITIAL_STATE = {
  espressoCount: 0,
  milkCount: 0,
  isContainChocolate: false,
};

const orderReducer = (state = INITIAL_STATE, action) => {
  switch (action.type) {
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
      const { espressoCount, milkCount, isContainChocolate } = action.payload;
      return { ...state, espressoCount, milkCount, isContainChocolate };
    }
    default:
      return { ...state };
  }
};
export default orderReducer;
