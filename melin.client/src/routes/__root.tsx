import { createRootRoute } from "@tanstack/react-router";
import Root from "@/root.tsx";
import { CookiesProvider } from "react-cookie";
import { TanStackRouterDevtools } from "@tanstack/router-devtools";

export const Route = createRootRoute({
    component: () => (
        <>
            <CookiesProvider>
                <Root children />
            </CookiesProvider>
            <TanStackRouterDevtools />
        </>
    ),
});
