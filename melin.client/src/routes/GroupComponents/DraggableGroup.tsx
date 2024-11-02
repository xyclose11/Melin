import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";
import {any, z} from "zod";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {Ellipsis} from "lucide-react";
import {EditGroupForm} from "@/routes/GroupComponents/EditGroupForm.tsx";

// const GroupSchema = z.object({
//     id: z.number(),
//     name: z.string(),
//     description: z.string().optional(),
//     references: z.array(baseReferenceSchema).optional()
// })

// type GroupType = z.infer<typeof GroupSchema>

const GroupNodeSchema = z.object({
    id: z.number(),
    name: z.string(),
    children: z.array(any()).optional()
})

type GroupNodeType = z.infer<typeof GroupNodeSchema>

export function DraggableGroup({
    groupName,
    groupNodes,
}: {
    groupName: string;
    groupNodes: [];
}) {
    
    return (
        <>
            <Card>
                <CardHeader>
                    <CardTitle>{groupName}</CardTitle>
                    <Popover>
                        <PopoverTrigger>
                            <Ellipsis />
                        </PopoverTrigger>
                        <PopoverContent>
                            <EditGroupForm groupName={groupName}/>
                        </PopoverContent>
                    </Popover>
                </CardHeader>
                <CardContent>
                    {groupNodes.map((gn: GroupNodeType) => (
                        <div key={gn.id}>
                            <div>
                                {gn.name}
                            </div>
                            <div>
                                {gn.children}
                            </div>
                        </div>
                    ))}
                </CardContent>
            </Card>
        </>
    );
}
