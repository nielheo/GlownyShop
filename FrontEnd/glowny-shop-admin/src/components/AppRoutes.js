import React from 'react'
import {
  BrowserRouter as Router,
  Route,
  IndexRoute,
  Link
} from 'react-router-dom'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import Home from '../components/Home'
import AboutUs from '../components/AboutUs'
import Layout from '../components/LayoutPage'

const BasicExample = () => (
  <MuiThemeProvider>
    <Router>
      <Layout>
        <Route exact path="/" component={Home}/>
        <Route path="/aboutus" component={AboutUs}/>
      </Layout>
    </Router>
  </MuiThemeProvider>
)


export default BasicExample