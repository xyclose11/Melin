import {
    Search,
    Settings,
    SquarePlus,
    PanelsTopLeft,
    Boxes,
    Tag,
    ChevronDown,
    Users,
    Target,
    FileSliders,
    Logs,
    Mailbox,
} from "lucide-react";
import {
    Sidebar,
    SidebarContent,
    SidebarGroup,
    SidebarGroupContent,
    SidebarGroupLabel,
    SidebarMenu,
    SidebarMenuBadge,
    SidebarMenuButton,
    SidebarMenuItem,
    SidebarMenuSub,
    SidebarMenuSubItem,
    SidebarSeparator,
} from "@/components/ui/sidebar.tsx";
import { Roles, useAuth } from "@/utils/AuthProvider.tsx";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "@/components/ui/collapsible.tsx";
import { useLocation } from "@tanstack/react-router";

const items = [
    {
        title: "Create",
        url: "/create-reference",
        icon: SquarePlus,
    },
    {
        title: "Library",
        url: "/library",
        icon: PanelsTopLeft,
    },
    {
        title: "Search",
        url: "/search",
        icon: Search,
    },
    {
        title: "Groups",
        url: "/groups",
        icon: Boxes,
    },
    {
        title: "Tags",
        url: "/tags",
        icon: Tag,
    },
    {
        title: "Settings",
        url: "#",
        icon: Settings,
    },
];
export function WorkspaceToolBar() {
    const { userRole } = useAuth();
    const location = useLocation();
    return (
        <Sidebar collapsible="icon" className={"flex-1 mt-16"}>
            <SidebarContent>
                <SidebarGroup>
                    <SidebarGroupLabel>Melin</SidebarGroupLabel>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {items.map((item) => (
                                <SidebarMenuItem key={item.title}>
                                    <SidebarMenuButton asChild>
                                        <a href={item.url}>
                                            <item.icon />
                                            <span>{item.title}</span>
                                        </a>
                                    </SidebarMenuButton>
                                </SidebarMenuItem>
                            ))}
                        </SidebarMenu>
                    </SidebarGroupContent>
                </SidebarGroup>
                {userRole === Roles.Admin &&
                location.pathname == "/admin-dashboard" ? (
                    <>
                        <SidebarSeparator />
                        <AdminSidebar />
                    </>
                ) : (
                    <></>
                )}
            </SidebarContent>
        </Sidebar>
    );
}

function AdminSidebar() {
    return (
        <SidebarGroup>
            <SidebarGroupContent>
                <SidebarMenu>
                    <Collapsible defaultOpen className="group/collapsible">
                        <SidebarMenuItem>
                            <CollapsibleTrigger asChild>
                                <SidebarMenuButton>
                                    <SidebarGroupLabel>
                                        Administrator Actions
                                    </SidebarGroupLabel>
                                    <SidebarMenuBadge>
                                        <ChevronDown className="ml-auto transition-transform group-data-[state=open]/collapsible:rotate-180" />
                                    </SidebarMenuBadge>
                                </SidebarMenuButton>
                            </CollapsibleTrigger>
                            <CollapsibleContent>
                                <SidebarMenuSub>
                                    <SidebarMenuSubItem>
                                        <SidebarMenuButton>
                                            Overview
                                            <SidebarMenuBadge>
                                                <Target strokeWidth={1} />
                                            </SidebarMenuBadge>
                                        </SidebarMenuButton>
                                    </SidebarMenuSubItem>
                                    <SidebarMenuSubItem>
                                        <SidebarMenuButton>
                                            Manage Users
                                            <SidebarMenuBadge>
                                                <Users strokeWidth={1} />
                                            </SidebarMenuBadge>
                                        </SidebarMenuButton>
                                    </SidebarMenuSubItem>
                                    <SidebarMenuSubItem>
                                        <SidebarMenuButton>
                                            Manage References
                                            <SidebarMenuBadge>
                                                <FileSliders strokeWidth={1} />
                                            </SidebarMenuBadge>
                                        </SidebarMenuButton>
                                    </SidebarMenuSubItem>
                                    <SidebarMenuSubItem>
                                        <SidebarMenuButton>
                                            View Logs
                                            <SidebarMenuBadge>
                                                <Logs strokeWidth={1} />
                                            </SidebarMenuBadge>
                                        </SidebarMenuButton>
                                    </SidebarMenuSubItem>
                                    <SidebarMenuSubItem>
                                        <SidebarMenuButton>
                                            View User Requests
                                            <SidebarMenuBadge>
                                                <Mailbox strokeWidth={1} />
                                            </SidebarMenuBadge>
                                        </SidebarMenuButton>
                                    </SidebarMenuSubItem>
                                </SidebarMenuSub>
                            </CollapsibleContent>
                        </SidebarMenuItem>
                    </Collapsible>
                </SidebarMenu>
            </SidebarGroupContent>
        </SidebarGroup>
    );
}
