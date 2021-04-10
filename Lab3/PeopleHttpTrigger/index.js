const PeopleDbContext = require('../DataAccess/db-context');
const common = require('./../common');



module.exports = async function (context, req) {
    const FirstName = req.query.name
    const LastName = req.query.LastName

    await common.functionWrapper(context, req, async (body) => {
        const connectionString = process.env['PeopleDb'];
        const peopleDb = new PeopleDbContext(connectionString, context.log);
        body.people = await peopleDb.deletePerson(FirstName);
    });
};