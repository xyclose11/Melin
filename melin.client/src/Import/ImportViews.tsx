import {
    Tabs,
    TabsList,
    TabsContent,
    TabsTrigger,
} from "@/components/ui/tabs.tsx";
import { FormattedView } from "@/Import/FormattedView.tsx";
import { RawView } from "@/Import/RawView.tsx";
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";
import { Button } from "@/components/ui/button.tsx";
import { ReferenceType } from "@/Import/ImportFile.tsx";

export function ImportViews({
    rawData,
    formattedData,
}: {
    rawData: string[];
    formattedData: ReferenceType[];
}) {
    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Parsed Data</CardTitle>

                    <CardDescription></CardDescription>
                </CardHeader>
                <CardContent>
                    <Tabs defaultValue="formatted" className="w-[400px]">
                        <TabsList>
                            <TabsTrigger value="formatted">
                                Formatted
                            </TabsTrigger>
                            <TabsTrigger value="raw">Raw</TabsTrigger>
                        </TabsList>
                        <TabsContent value="formatted">
                            {formattedData.map((f) => (
                                <FormattedView data={f} />
                            ))}
                        </TabsContent>
                        <TabsContent value="raw">
                            {rawData.map((r) => (
                                <RawView rawData={r} />
                            ))}
                        </TabsContent>
                    </Tabs>
                </CardContent>
                <CardFooter>
                    <Button variant="destructive">Cancel</Button>
                    <Button variant="secondary">Import</Button>
                </CardFooter>
            </Card>
        </div>
    );
}
