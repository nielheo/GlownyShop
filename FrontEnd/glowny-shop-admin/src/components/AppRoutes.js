import React from 'react'
import {
  BrowserRouter as Router,
  Route,
  IndexRoute,
  Link,
  Switch,
  Redirect
} from 'react-router-dom'
import { Provider } from 'react-redux'
import store from '../store/store.js'
import {grey900, cyan500} from 'material-ui/styles/colors'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import getMuiTheme from 'material-ui/styles/getMuiTheme'
import { getUserToken } from '../components/Common/Cookies'
//import EnsureLoggedInContainer from '../components/Common/EnsureLoggedInContainer'
import Layout from '../components/Layout'
import Home from '../components/Home'
import Login from '../components/Login'
import Logout from '../components/Logout'
import NotFound from '../components/NotFound'
import AboutUs from '../components/AboutUs'
import Forgot from '../components/Forgot' 

const muiTheme = getMuiTheme({
  palette: {
    //textColor: grey900,
    //primary1Color: grey900,
    //accent1Color: cyan500,
  },
})

class AppRoutes extends React.Component {
  render() {
    return (
      /*!isLoggedIn && location.pathname !== '/login' ?
        <Redirect to='/login'/>
      :*/
      <Provider store={store}>
        <MuiThemeProvider muiTheme={muiTheme}>
          <Router>
            <Layout>
              <Switch>
                <Route path="/login" component={Login}/>
                <Route path="/logout" component={Logout}/>
                <Route path="/forgotpassword" component={Forgot}/>
                <Route path="/404" component={NotFound}/>
                <PrivateRoute exact path="/" component={Home} />
                <PrivateRoute path="/aboutus" component={AboutUs} />
                <PrivateNotFound to='/404'/>
              </Switch>
            </Layout>
          </Router>
        </MuiThemeProvider>
      </Provider>
    )
  }
}

const PrivateNotFound = ({to, ...rest }) => (
  <Route {...rest} render={props => (
    getUserToken() ? (
      <Redirect to={{
        pathname: to,
        state: { from: props.location }
      }}/>
    ) : (
      <Redirect to={{
        pathname: '/login',
        state: { from: props.location }
      }}/>
    )
  )}/>
)

const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={props => (
    getUserToken('User_Token') ? (
      <Component {...props}/>
    ) : (
      <Redirect to={{
        pathname: '/login',
        state: { from: props.location }
      }}/>
    )
  )}/>
)

export default AppRoutes