import {createBrowserRouter, RouterProvider} from "react-router-dom";
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import Root from "./routes/root.tsx";
import ErrorPage from "./error-page.tsx";
import Contact from "./routes/contact.tsx";
import SignUp from "@/routes/SignUp.tsx";

const router: any = createBrowserRouter([{
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
        {
            path: "contacts/:contactId",
            element: <Contact />,
        },
        {
            path: "sign-up",
            element: <SignUp />
        }
    ]
}
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      {/*<BrowserRouter>*/}
      <RouterProvider router={router}/>
          <App />
      {/*</BrowserRouter>*/}
  </StrictMode>,
)
