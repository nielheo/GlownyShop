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
import { getUserToken } from '../components/Common/Cookies'
//import EnsureLoggedInContainer from '../components/Common/EnsureLoggedInContainer'
import Layout from '../components/Layout'
import Home from '../components/Home'
import Login from '../components/Login'
import Logout from '../components/Logout'
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
              <Route path="/login" component={Login}/>
              <Route path="/logout" component={Logout}/>
              <Route path="/404" component={NotFound}/>
              <PrivateRoute exact path="/" component={Home} />
              <PrivateRoute path="/aboutus" component={AboutUs} />
              <Redirect to='/404'/>
            </Switch>
          </Layout>
        </Router>
      </MuiThemeProvider>
    )
  }
}

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