import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import { z } from "zod";
import { EllipsisVertical } from "lucide-react";
import { EditGroupForm } from "@/routes/GroupComponents/EditGroupForm.tsx";
import { Button } from "@/components/ui/button.tsx";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from "@/components/ui/dialog.tsx";
import { useReferenceSelection } from "@/routes/Context/ReferencesSelectedContext.tsx";
import { instance } from "@/utils/axiosInstance.ts";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
    DropdownMenuSeparator,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuSub,
} from "@/components/ui/dropdown-menu.tsx";

const GroupNodeSchema = z.object({
    id: z.number(),
    title: z.string(),
});

type GroupNodeType = z.infer<typeof GroupNodeSchema>;

export function DraggableGroup({
    groupName,
    groupNodes,
}: {
    groupName: string;
    groupNodes: [];
}) {
    const { selectedReferences } = useReferenceSelection();

    const handleAddReferences = async () => {
        try {
            const res = await instance.post(
                `add-refs-to-group?groupName=${groupName}`,
                selectedReferences,
                {
                    withCredentials: true,
                },
            );

            if (res.status === 200) {
                // TODO display sonner
                console.log("SUCCESS");
            } else {
                console.error("UNABLE TO ADD REFERENCES TO GROUP");
            }
        } catch (e) {
            console.error(e);
        }
    };
    return (
        <>
            <Card>
                <CardHeader className={"flex"}>
                    <CardTitle>{groupName}</CardTitle>
                    <Dialog>
                        <DropdownMenu>
                            <DropdownMenuTrigger>
                                <Button
                                    size={"icon"}
                                    className={" p-1 h-fit w-fit"}
                                >
                                    <EllipsisVertical size={12} />
                                </Button>
                            </DropdownMenuTrigger>
                            <DropdownMenuContent>
                                <DropdownMenuSub>
                                    <DropdownMenuSubTrigger>
                                        Edit Details
                                    </DropdownMenuSubTrigger>
                                    <DropdownMenuSubContent>
                                        <EditGroupForm groupName={groupName} />a
                                    </DropdownMenuSubContent>
                                </DropdownMenuSub>
                                <DropdownMenuSeparator />
                                <DialogTrigger>
                                    <DropdownMenuItem>
                                        Add References
                                    </DropdownMenuItem>
                                </DialogTrigger>
                            </DropdownMenuContent>
                        </DropdownMenu>
                        <DialogContent>
                            <DialogHeader>
                                <DialogTitle>
                                    Adding References into {groupName}
                                </DialogTitle>
                                <DialogDescription>
                                    Add references and/or groups into{" "}
                                    {groupName}. Current limit is 1,000 Nodes
                                    Total
                                </DialogDescription>
                            </DialogHeader>
                            <DialogFooter>
                                <Button
                                    type="submit"
                                    onClick={handleAddReferences}
                                >
                                    Confirm
                                </Button>
                            </DialogFooter>
                        </DialogContent>
                    </Dialog>
                </CardHeader>
                <CardContent>
                    {groupNodes.map((gn: GroupNodeType) => (
                        <div key={gn.id}>
                            {" "}
                            {/* TODO make these draggable and collapsible */}
                            <div>{gn.title}</div>
                        </div>
                    ))}
                </CardContent>
            </Card>
        </>
    );
}
