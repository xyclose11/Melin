import {
    Tabs,
    TabsList,
    TabsContent,
    TabsTrigger,
} from "@/components/ui/tabs.tsx";
import { FormattedView } from "@/Import/FormattedView.tsx";
import { RawView } from "@/Import/RawView.tsx";

export function ImportViews() {
    return (
        <div>
            <h1>Parsed Data</h1>
            <Tabs defaultValue="formatted" className="w-[400px]">
                <TabsList>
                    <TabsTrigger value="formatted">Formatted</TabsTrigger>
                    <TabsTrigger value="raw">Raw</TabsTrigger>
                </TabsList>
                <TabsContent value="formatted">
                    <FormattedView />
                </TabsContent>
                <TabsContent value="raw">
                    <RawView />
                </TabsContent>
            </Tabs>
        </div>
    );
}
