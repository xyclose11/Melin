import fs from 'fs/promises';
import path from 'path';

export default async function handler(req, res) {
    try {
        console.log("HIO")
        const data = await fs.readFile(
            path.join(process.cwd(), "./data/tasks.json"),
            "utf-8"
        );
        console.log(data)
        res.status(200).json(JSON.parse(data));
    } catch (err) {
        res.status(500).json({ error: 'Error reading tasks' });
    }
}
