const defaultState = {
  fetching: false,
  data: {}
}

export default function(state = defaultState, action) {
  switch (action.type) {
    case 'STARTING_REQUEST':
      return {
        ...state,
        fetching: true,
      }
    case 'FINISHED_REQUEST':
      return {
        ...state,
        fetching: false,
        data: action.response.data.viewer,
      }
    default:
      return state
  }
}