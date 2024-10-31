import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import Root from "./routes/root.tsx";
import ErrorPage from "./error-page.tsx";
import Contact from "./routes/contact.tsx";
import SignUp from "@/routes/SignUp.tsx";
import { LoginForm } from "@/routes/Login.tsx";
import { LibraryPage } from "@/routes/Library.tsx";
import UserSettings from "@/routes/UserSettingsPage.tsx";
import { AuthProvider } from "@/utils/AuthProvider.tsx";
import { CreateReferencePage } from "@/routes/CreateReferencePage.tsx";
import { ThemeProvider } from "@/components/theme-provider.tsx";
import PrivateRoute from "./utils/PrivateRoute.tsx";
import { GroupLibrary } from "@/routes/LibraryViews/GroupLibrary.tsx";
import { TagLibrary } from "@/routes/LibraryViews/TagLibrary.tsx";
import { HomePage } from "@/routes/HomePage.tsx";

const router: any = createBrowserRouter([
    {
        path: "/",
        element: <Root children />,
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
                element: <UserSettings />,
            },
            {
                path: "library-settings",
            },
            {
                path: "create-reference",
                element: <CreateReferencePage />,
            },
        ],
    },
]);

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <AuthProvider>
            <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                <RouterProvider router={router} />
            </ThemeProvider>
        </AuthProvider>
    </StrictMode>,
);
