import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material"
import { tuteeResponse } from "../../types/TuteeResponse"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/tutees/${slug}`, { cache: 'no-store' })
  const tutee = (await data.json()) as tuteeResponse

  function getDate(dateStr: string) {
    const date = new Date(dateStr)
    const formattedMonth = `${date.getMonth() + 1}`.padStart(2, '0')
    const formattedDay = `${date.getDate()}`.padStart(2, '0')
    return `${formattedDay}/${formattedMonth}/${date.getFullYear()}`
  }

  function getTime(timeStr: string) {
    const date = new Date(timeStr)
    const formattedHours = `${date.getHours()}`.padStart(2, '0')
    const formattedMinutes = `${date.getMinutes()}`.padStart(2, '0')
    return `${formattedHours}:${formattedMinutes}`
  }

  return (
    <div>
      <Typography variant="h1" align="center">{tutee.firstName} {tutee.lastName} 🎓</Typography>
      <Typography variant="h3">Lessons</Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Date</TableCell>
              <TableCell>Start</TableCell>
              <TableCell>End</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {tutee.lessons.map((l, i) => 
              <TableRow key={i}>
                <TableCell>
                  <Typography>{getDate(l.startTime)}</Typography>
                </TableCell>
                <TableCell>
                  <Typography>{getTime(l.startTime)}</Typography>
                </TableCell>
                <TableCell>
                  <Typography>{getTime(l.endTime)}</Typography>
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  )
}