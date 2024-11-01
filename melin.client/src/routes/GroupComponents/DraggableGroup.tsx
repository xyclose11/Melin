import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card.tsx";

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
                </CardHeader>
                <CardContent>
                    {groupNodes.map((gn) => (
                        <div>{gn}</div>
                    ))}
                </CardContent>
            </Card>
        </>
    );
}
