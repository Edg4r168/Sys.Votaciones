import "./App.css";
import { LoginPageAdmin } from "./Pages/Admin/Login";
import { RegisterPage } from "./Pages/User/Register";
import { LoginPageUser } from "./Pages/User/Login";
import { Route } from "wouter";
import { Landing } from "./Pages/User/Landing/Landing";

function App() {
  return (
    <>
      {/* User routes */}
      <Route path="/" component={Landing} />
      <Route path="/register" component={RegisterPage} />
      <Route path="/login-user" component={LoginPageUser} />

      {/* Admin routes */}
      <Route path="/login-admin" component={LoginPageAdmin} />
    </>
  );
}

export default App;
