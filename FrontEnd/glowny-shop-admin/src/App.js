'use strict';
import React from 'react';
import AppRoutes from './components/AppRoutes';

export default class App extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      isLoading: true,
      token: '',
    };
  }

 

  _updateToken = (token) => {
    localStorage.setItem('token', token);

    /*if (token === '' || token === null) {
      const networkLayer = new NetworkLayer(backendUrlGraphql, {});
      RelayStore.reset(networkLayer);
      localStorage.clear();

    } else {
      const networkLayer = new NetworkLayer(
        backendUrlGraphql, {
          headers: {
            token: token,
          },
      });

      networkLayer.setToken(token);
      RelayStore.reset(networkLayer);

    }

    this.setState({
      token,
    });
    */
    // Router.refresh;

  };

  render() {
    return (
      <AppRoutes 
        updateToken={this._updateToken}
      />
    );
  }
}
