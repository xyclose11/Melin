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
            <Card className="h-[100%] rounded-br-none rounded-tr-none">
                <CardHeader className={"flex p-4 justify-center"}>
                    <CardTitle className="text-sm p-1 bg-secondary rounded">
                        Groups
                    </CardTitle>
                    <DropdownMenu>
                        <DropdownMenuTrigger className="justify-end">
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

                <CardContent className="flex flex-col flex-wrap">
                    {children}
                </CardContent>

                <CardFooter></CardFooter>
            </Card>
        </>
    );
}
