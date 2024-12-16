import { createRouter, RouterProvider } from "@tanstack/react-router";
import ReactDOM from "react-dom/client";
import { routeTree } from "./routeTree.gen";
import { AuthProvider } from "@/utils/AuthProvider.tsx";
import { ThemeProvider } from "@/components/theme-provider.tsx";
import "./index.css";

const router = createRouter({ routeTree });

declare module "@tanstack/react-router" {
    interface Register {
        router: typeof router;
    }
}

const rootElement = document.getElementById("root")!;
if (!rootElement.innerHTML) {
    const root = ReactDOM.createRoot(rootElement);
    root.render(
        <AuthProvider>
            <ThemeProvider defaultTheme={"dark"} storageKey={"vite-ui-theme"}>
                <RouterProvider router={router} />
            </ThemeProvider>
        </AuthProvider>,
    );
}
