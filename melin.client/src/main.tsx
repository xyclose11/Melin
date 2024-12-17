import { createRouter, RouterProvider } from "@tanstack/react-router";
import ReactDOM from "react-dom/client";
import { routeTree } from "./routeTree.gen";
import { AuthProvider, useAuth } from "@/utils/AuthProvider.tsx";
import { ThemeProvider } from "@/components/theme-provider.tsx";
import "./index.css";
import NotFoundPage from "@/NotFoundPage.tsx";

const router = createRouter({
    routeTree,
    defaultPreload: "intent",
    context: {
        auth: undefined,
    },
    defaultNotFoundComponent: NotFoundPage,
});

declare module "@tanstack/react-router" {
    interface Register {
        router: typeof router;
    }
}

function InnerApp() {
    const auth = useAuth();
    return <RouterProvider router={router} context={{ auth }}></RouterProvider>;
}

function App() {
    return (
        <AuthProvider>
            <InnerApp />
        </AuthProvider>
    );
}

const rootElement = document.getElementById("root")!;
if (!rootElement.innerHTML) {
    const root = ReactDOM.createRoot(rootElement);
    root.render(
        <ThemeProvider defaultTheme={"dark"} storageKey={"vite-ui-theme"}>
            <App />
        </ThemeProvider>,
    );
}
