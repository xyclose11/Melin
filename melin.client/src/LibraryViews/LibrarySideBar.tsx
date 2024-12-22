import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";

import { CreateGroupForm } from "@/GroupComponents/CreateGroupForm.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuSub,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";
import { EllipsisVertical } from "lucide-react";
import { Group } from "@/LibraryViews/GroupLibrary.tsx";

export function LibrarySideBar({
    children,
    handleAddToUserGroup,
}: {
    children: React.ReactNode;
    handleAddToUserGroup: (newGroup: Group) => void;
}) {
    return (
        <>
            <Card>
                <CardHeader className={"flex"}>
                    <CardTitle>Groups</CardTitle>
                    <DropdownMenu>
                        <DropdownMenuTrigger>
                            <EllipsisVertical size={14} />
                        </DropdownMenuTrigger>
                        <DropdownMenuContent>
                            <DropdownMenuSub>
                                <DropdownMenuSubTrigger>
                                    Create Group
                                </DropdownMenuSubTrigger>
                                <DropdownMenuSubContent>
                                    <CreateGroupForm
                                        handleAddToUserGroup={
                                            handleAddToUserGroup
                                        }
                                    />
                                </DropdownMenuSubContent>
                            </DropdownMenuSub>
                        </DropdownMenuContent>
                    </DropdownMenu>
                </CardHeader>

                <CardContent>{children}</CardContent>

                <CardFooter></CardFooter>
            </Card>
        </>
    );
}
