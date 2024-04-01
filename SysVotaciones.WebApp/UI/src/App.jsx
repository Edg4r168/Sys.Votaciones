import "./App.css";
import "src/Components/Notification/index.css";

import { LoginPageAdmin } from "./Pages/Admin/Login";
import { RegisterPage } from "./Pages/User/Register";
import { LoginPageUser } from "./Pages/User/Login";
import { Route, Router } from "wouter";
import { Landing } from "./Pages/User/Landing/Landing";
import { Home } from "./Pages/User/Home";
import { UserProvider } from "./context/userContext";
import { AdminProvider } from "./context/adminContext";
// import { Table } from "./Components/Table/Table";
import { TCategory } from "./Components/Admin/TCategory/TCategory";
import { Dashboard } from "./Components/Admin/Dashboard/Dashboard";
import { Modal } from "./Components/Modal/Modal";

function App() {
  return (
    <>
      <UserProvider>
        {/* User routes */}
        <Route path="/" component={Landing} />
        <Route path="/register" component={RegisterPage} />
        <Route path="/login-user" component={LoginPageUser} />
        <Route path="/home" component={Home} />
      </UserProvider>

      <Route path="/modal" component={Modal} />

      <Route path="/table" component={TCategory} />

      <AdminProvider>
        {/* Admin routes */}
        <Route path="/login-admin" component={LoginPageAdmin} />

        <Router base="/dashboard">
          <Route path="/:section?" component={Dashboard} />
        </Router>
      </AdminProvider>
    </>
  );
}

export default App;
