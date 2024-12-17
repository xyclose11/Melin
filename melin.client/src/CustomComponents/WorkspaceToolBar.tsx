import {
    Search,
    Settings,
    SquarePlus,
    PanelsTopLeft,
    Boxes,
    Tag,
    ChevronDown,
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
} from "@/components/ui/sidebar.tsx";
import { Roles, useAuth } from "@/utils/AuthProvider.tsx";
import {
    Collapsible,
    CollapsibleContent,
    CollapsibleTrigger,
} from "@/components/ui/collapsible.tsx";

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
        url: "#",
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
    return (
        <Sidebar className={"flex-1 mt-16"}>
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
                        {userRole === Roles.Admin ? <AdminSidebar /> : <></>}
                    </SidebarGroupContent>
                </SidebarGroup>
            </SidebarContent>
        </Sidebar>
    );
}

function AdminSidebar() {
    return (
        <SidebarMenu>
            <Collapsible defaultOpen className="group/collapsible">
                <SidebarMenuItem>
                    <CollapsibleTrigger asChild>
                        <SidebarMenuButton>
                            Admin Tools
                            <SidebarMenuBadge>
                                <ChevronDown className="ml-auto transition-transform group-data-[state=open]/collapsible:rotate-180" />
                            </SidebarMenuBadge>
                        </SidebarMenuButton>
                    </CollapsibleTrigger>
                    <CollapsibleContent>
                        <SidebarMenuSub>
                            <SidebarMenuSubItem>
                                HI
                                <SidebarMenuBadge>2</SidebarMenuBadge>
                            </SidebarMenuSubItem>
                        </SidebarMenuSub>
                    </CollapsibleContent>
                </SidebarMenuItem>
            </Collapsible>
        </SidebarMenu>
    );
}
