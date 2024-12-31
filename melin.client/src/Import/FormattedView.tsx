import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";
import { ReferenceType } from "@/Import/ImportFile.tsx";

export function FormattedView({ data }: { data: ReferenceType }) {
    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Formatted View</CardTitle>
                    <CardDescription>
                        Here is the formatted view!
                    </CardDescription>
                </CardHeader>
                <CardContent>{data.title}</CardContent>
                <CardFooter>FOOTER</CardFooter>
            </Card>
        </div>
    );
}
