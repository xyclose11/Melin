import { Link } from "@tanstack/react-router";

import { CircleUser, Search, SquareLibrary } from "lucide-react";

import { Button } from "@/components/ui/button";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Input } from "@/components/ui/input";
import LogoutLink from "@/CustomComponents/LogoutLink.tsx";
import UserSettingsLink from "@/CustomComponents/UserSettingsLink.tsx";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { ModeToggle } from "@/components/mode-toggle.tsx";

export function NavBar() {
    const { isAuthenticated, userRole } = useAuth();

    return (
        <header className="z-20 fixed w-full top-0 p-6 flex justify-center h-16 items-center gap-4 border-b bg-background">
            <nav className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
                <Link
                    to={"/"}
                    className="flex items-center gap-2 text-lg font-semibold md:text-base"
                >
                    <SquareLibrary className="h-6 w-6" />
                    <span className="sr-only">Melin</span>
                </Link>
                <Link
                    to={"/"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Dashboard
                </Link>
                <Link
                    to={"/"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Review
                </Link>
                <Link
                    to={"/library"}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Library
                </Link>
                <ModeToggle />
            </nav>
            <div className="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
                <form className="ml-auto flex-1 sm:flex-initial">
                    <div className="relative">
                        <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
                        <Input
                            type="search"
                            placeholder="Search..."
                            className="pl-8 sm:w-[300px] md:w-[200px] lg:w-[300px]"
                        />
                    </div>
                </form>
                <DropdownMenu>
                    <DropdownMenuTrigger className="mr-0.5" asChild>
                        <Button
                            variant="secondary"
                            size="icon"
                            className="rounded-full"
                        >
                            <CircleUser className="h-5 w-5" />
                            <span className="sr-only">Toggle user menu</span>
                        </Button>
                    </DropdownMenuTrigger>

                    <DropdownMenuContent align="end">
                        {!isAuthenticated ? (
                            <DropdownMenuItem>
                                <Link
                                    to="/login"
                                    className="text-muted-foreground transition-colors hover:text-foreground"
                                >
                                    Login
                                </Link>
                            </DropdownMenuItem>
                        ) : (
                            <>
                                <DropdownMenuLabel>
                                    My Account
                                </DropdownMenuLabel>
                                <DropdownMenuSeparator />
                                <DropdownMenuItem>
                                    <UserSettingsLink />
                                </DropdownMenuItem>
                                <DropdownMenuSeparator />
                                {userRole === "ADMIN" ? (
                                    <div>
                                        <DropdownMenuItem>
                                            <Link
                                                to={"/admin-dashboard"}
                                                className="text-muted-foreground transition-colors hover:text-foreground"
                                            >
                                                Admin Dashboard
                                            </Link>
                                        </DropdownMenuItem>
                                        <DropdownMenuSeparator />
                                    </div>
                                ) : (
                                    <DropdownMenuItem></DropdownMenuItem>
                                )}
                                <DropdownMenuItem>
                                    <LogoutLink />
                                </DropdownMenuItem>
                            </>
                        )}
                    </DropdownMenuContent>
                </DropdownMenu>
            </div>
        </header>
    );
}
