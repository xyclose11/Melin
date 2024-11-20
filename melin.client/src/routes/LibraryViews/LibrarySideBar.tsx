import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";

import { CreateGroupForm } from "@/routes/GroupComponents/CreateGroupForm.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuSub,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";
import { EllipsisVertical } from "lucide-react";

export function LibrarySideBar(props: any) {
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
                                    <CreateGroupForm />
                                </DropdownMenuSubContent>
                            </DropdownMenuSub>
                        </DropdownMenuContent>
                    </DropdownMenu>
                </CardHeader>

                <CardContent>{props.children}</CardContent>

                <CardFooter></CardFooter>
            </Card>
        </>
    );
}
