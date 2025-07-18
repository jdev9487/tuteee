import Link from "next/link"
import { tuteeResponse } from "../types/TuteeResponse"
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material"

export default async function Page() {
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/tutees`, { cache: 'no-store' })
  const tutees = (await data.json()) as tuteeResponse[]
  return (
    <div>
      <Typography variant="h1" align="center">Tutees</Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Email address</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {tutees.map((t, i) => 
              <TableRow key={i}>
                <TableCell>
                  <Typography><Link href={`/tutees/${t.tuteeId}`}>{`${t.firstName} ${t.lastName}`}</Link></Typography>
                </TableCell>
                <TableCell>
                  <Typography>{t.emailAddress}</Typography>
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  )
}