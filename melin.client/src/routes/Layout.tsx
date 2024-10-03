import {Form, Link} from "react-router-dom";

import {CircleUser, Search, SquareLibrary} from "lucide-react"

import { Button } from "@/components/ui/button"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Input } from "@/components/ui/input"
import {instance} from "@/utils/axiosInstance.ts";

export const description =
    "A settings page. The settings page has a sidebar navigation and a main content area. The main content area has a form to update the store name and a form to update the plugins directory. The sidebar navigation has links to general, security, integrations, support, organizations, and advanced settings."


export function NavBar() {
    const logout = async (e: any)=> {
        e.preventDefault();
        try {
            await instance.post(`https://localhost:5000/api/auth/logout`)
                // TODO CHANGE THE ABOVE URL TO BE DYNAMIC FOR THE SERVER IT IS SET ON
                .then(function (response) {
                    console.log(response)
                });
        } catch (error) {
            console.error('Logout failed:', error);
        }
    };
    
    // LAST WORKING 10/2/2024 ON SETTING UP ROLES (ADMIN, USER, GUEST) SO THAT EACH DISPLAYS DIFFERENT LAYOUTS, ETC.
    
    return (
        <div className="flex w-full flex-col">
        <header className="fixed top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-6">
            <nav
                className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
                <Link
                    to={'#'}
                    className="flex items-center gap-2 text-lg font-semibold md:text-base"
                >
                    <SquareLibrary className="h-6 w-6" />
                    <span className="sr-only">Melin</span>
                </Link>
                <Link
                    to={'/'}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Home
                </Link>
                <Link
                    to={'/dashboard'}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Dashboard
                </Link>
                <Link
                    to={'/groups'}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Groups
                </Link>
                <Link
                    to={'/library'}
                    className="text-foreground transition-colors hover:text-foreground"
                >
                    Library
                </Link>
            </nav>
            <div className="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
                <form className="ml-auto flex-1 sm:flex-initial">
                    <div className="relative">
                        <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground"/>
                        <Input
                            type="search"
                            placeholder="Search..."
                            className="pl-8 sm:w-[300px] md:w-[200px] lg:w-[300px]"
                        />
                    </div>
                </form>
                <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                        <Button variant="secondary" size="icon" className="rounded-full">
                            <CircleUser className="h-5 w-5"/>
                            <span className="sr-only">Toggle user menu</span>
                        </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                        <DropdownMenuLabel>My Account</DropdownMenuLabel>
                        <DropdownMenuSeparator/>
                        <DropdownMenuItem>
                            <Link
                                to={'/user-settings'}
                                className="text-muted-foreground transition-colors hover:text-foreground"
                            >
                                User Settings
                            </Link>
                        </DropdownMenuItem>
                        <DropdownMenuSeparator/>
                        <DropdownMenuItem>
                            <Form onSubmit={logout}
                                  className="text-muted-foreground transition-colors hover:text-foreground"
                            >
                                <Button type="submit">
                                    Logout
                                </Button>
                            </Form>
                        </DropdownMenuItem>
                    </DropdownMenuContent>
                </DropdownMenu>
            </div>
        </header>
        </div>
    )
}
