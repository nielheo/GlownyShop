import React from 'react';
import ReactDOM from 'react-dom'
import App from './App'
import AppRoutes from './components/AppRoutes'
import injectTapEventPlugin from 'react-tap-event-plugin'
injectTapEventPlugin();

ReactDOM.render(
  <AppRoutes />,
  document.getElementById('root')
);
