import React from 'react'
import {
  BrowserRouter as Router,
  Route,
  IndexRoute,
  Link,
  Switch,
  Redirect
} from 'react-router-dom'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import Layout from '../components/Layout'
import Home from '../components/Home'
import Login from '../components/Login'
import NotFound from '../components/NotFound'
import AboutUs from '../components/AboutUs'


const BasicExample = () => (
  <MuiThemeProvider>
    <Router>
      <Layout>
        <Switch>
          <Route exact path="/" component={Home}/>
          <Route path="/aboutus" component={AboutUs}/>
          <Route path="/login" component={Login}/>
          <Route path="/404" component={NotFound}/>
          
          <Redirect to='/404'/>
        </Switch>
      </Layout>
    </Router>
  </MuiThemeProvider>
)


export default BasicExample