import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { createRoot } from "react-dom/client";
import "./index.css";
import Root from "./routes/root.tsx";
import ErrorPage from "./error-page.tsx";
import Contact from "./routes/contact.tsx";
import SignUp from "@/routes/SignUp.tsx";
import { LoginForm } from "@/routes/Login.tsx";
import { LibraryPage } from "@/routes/LibraryPage.tsx";
import UserSettings from "@/routes/UserSettingsPage.tsx";
import { AuthProvider } from "@/utils/AuthProvider.tsx";
import { CreateReferencePage } from "@/routes/CreateReferencePage.tsx";
import { ThemeProvider } from "@/components/theme-provider.tsx";
import PrivateRoute from "./utils/PrivateRoute.tsx";
import { GroupLibrary } from "@/routes/LibraryViews/GroupLibrary.tsx";
import { TagLibrary } from "@/routes/LibraryViews/TagLibrary.tsx";
import { HomePage } from "@/routes/HomePage.tsx";
import { EditReferencePage } from "@/routes/CustomComponents/Reference/EditReferencePage.tsx";
import { CookiesProvider } from "react-cookie";

const router: any = createBrowserRouter([
    {
        path: "/",
        element: (
            <CookiesProvider>
                <Root children />
            </CookiesProvider>
        ),
        errorElement: <ErrorPage />,
        children: [
            {
                path: "/home",
                element: <HomePage />,
            },
            {
                path: "contacts/:contactId",
                element: <Contact />,
            },
            {
                path: "sign-up",
                element: <SignUp />,
            },
            {
                path: "login",
                element: <LoginForm />,
            },
            {
                path: "logout",
            },
            {
                path: "reset-password",
            },
            {
                path: "dashboard",
            },
            {
                path: "groups",
                element: (
                    <PrivateRoute
                        element={<GroupLibrary></GroupLibrary>}
                    ></PrivateRoute>
                ),
            },
            {
                path: "tags",
                element: (
                    <PrivateRoute
                        element={<TagLibrary></TagLibrary>}
                    ></PrivateRoute>
                ),
            },
            {
                path: "library",
                element: (
                    <PrivateRoute
                        element={<LibraryPage></LibraryPage>}
                    ></PrivateRoute>
                ),
            },
            {
                path: "user-settings",
                element: (
                    <PrivateRoute element={<UserSettings />}></PrivateRoute>
                ),
            },
            {
                path: "library-settings",
            },
            {
                path: "create-reference",
                element: <PrivateRoute element={<CreateReferencePage />} />,
            },
            {
                path: "edit-reference/:refId",
                element: <PrivateRoute element={<EditReferencePage />} />,
            },
        ],
    },
]);

createRoot(document.getElementById("root")!).render(
    <AuthProvider>
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <RouterProvider router={router} />
        </ThemeProvider>
    </AuthProvider>,
);
