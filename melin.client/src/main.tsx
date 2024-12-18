import { createRouter, RouterProvider } from "@tanstack/react-router";
import ReactDOM from "react-dom/client";
import { routeTree } from "./routeTree.gen";
import { AuthProvider, useAuth } from "@/utils/AuthProvider.tsx";
import { ThemeProvider } from "@/components/theme-provider.tsx";
import "./index.css";
import NotFoundPage from "@/NotFoundPage.tsx";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const router = createRouter({
    routeTree,
    defaultPreload: "intent",
    context: {
        auth: undefined,
    },
    defaultNotFoundComponent: NotFoundPage,
});

const queryClient = new QueryClient();

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
        <QueryClientProvider client={queryClient}>
            <AuthProvider>
                <InnerApp />
            </AuthProvider>
        </QueryClientProvider>
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
