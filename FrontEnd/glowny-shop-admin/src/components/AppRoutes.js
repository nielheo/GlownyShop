import React from 'react'
import {
  BrowserRouter as Router,
  Route,
  IndexRoute,
  Link,
  Switch,
  Redirect
} from 'react-router-dom'
import {grey900, cyan500} from 'material-ui/styles/colors'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import getMuiTheme from 'material-ui/styles/getMuiTheme'

import Layout from '../components/Layout'
import Home from '../components/Home'
import Login from '../components/Login'
import NotFound from '../components/NotFound'
import AboutUs from '../components/AboutUs'

const muiTheme = getMuiTheme({
  palette: {
    textColor: grey900,
    primary1Color: grey900,
    accent1Color: cyan500,
  },
})

class AppRoutes extends React.Component {
  render() {
    return (
      /*!isLoggedIn && location.pathname !== '/login' ?
        <Redirect to='/login'/>
      :*/
      <MuiThemeProvider muiTheme={muiTheme}>
        <Router>
          <Layout>
            <Switch>
              <Route path="/login" 
                render={(props) => <Login {...props} 
                  updateToken={this.props.updateToken} 
                  />}
              />
              <Route path="/404" component={NotFound}/>
              <PrivateRoute path="/" component={Home} isAuthenticated={_isAuthenticated()}/>
              <PrivateRoute path="/aboutus" component={AboutUs} isAuthenticated={_isAuthenticated()}/>
              <Redirect to='/404'/>
            </Switch>
          </Layout>
        </Router>
      </MuiThemeProvider>
    )
  }
}

const _isAuthenticated = async () => {
  let token;
  try {
    token = await localStorage.getItem('token')
  } catch (err) {
    token = '';
  }

  return token !== null && token !== '';
}

const PrivateRoute = ({ component: Component, ...rest, isAuthenticated }) => (
  <Route {...rest} render={props => (
    _isAuthenticated() ? (
      <Component {...props}/>
    ) : (
      <Redirect to={{
        pathname: '/login',
        state: { from: props.location }
      }}/>
    )
  )}/>
)


export default AppRoutes//withRouter(AppRoutes)