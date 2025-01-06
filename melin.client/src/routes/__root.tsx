import { createRootRoute } from "@tanstack/react-router";
import Root from "@/root.tsx";
import { CookiesProvider } from "react-cookie";
import { TanStackRouterDevtools } from "@tanstack/router-devtools";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { Footer } from "@/Footer.tsx";

export const Route = createRootRoute({
    component: () => (
        <div>
            <CookiesProvider>
                <Root children />
            </CookiesProvider>
            <Footer />

            <TanStackRouterDevtools />
            <ReactQueryDevtools />
        </div>
    ),
});
