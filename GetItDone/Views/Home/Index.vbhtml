﻿@Code
    ViewData("Title") = "Home Page"
End Code

<h2>Isn't it time to get to done?</h2>
<p>Done is the state we all want to be in. This site was made to help me - and you - get to done. Based on the methods described in David Allen's book Getting Things Done, I've set out on a journey to create the tool that will force me to work in a GTD manner, rather than all the tools that are out there that merely allow me to do so.</p>

<p>Right now, @ViewBag.PeopleCount people have an account.</p>

<h2>New on the site:</h2>
<p>2012-10-03: Small adjustments, mostly. Moved the offline tab off the main page - did anyone ever use it? Reinserted the projects page as a tab. Fixed project page so it only lists active tasks for each project.</p>
<p>2012-09-26: Made a notification on the review tab if you've gone more than 7 days without a review. If you hover over the notification, it'll tell you when your last review was. Also made it possible to log in using either email or user name.</p>
<p>2012-09-25: Added a feedback button - look at the right side of the page. It's using <a href="http://uservoice.com">UserVoice</a>, because that seemed like a nice solution for support. I've received a few suggestions in my inbox that I'm currently evaluating, so maybe I'll be able to post about some real news on this site soonish.</p>
<p>2012-08-07: Added hotkey info on the collection page, an edit button on the process page and improved the flow of some actions. Also, <a href="http://resources.gettodone.dk/gettodone.apk">a new version of the android app is available</a>! You will probably need to uninstall the old app before installing the new version. Sorry.</p>
<p>2012-08-04: Selecting a project in the auto-complete in collect causes the task to be assigned to that project. Escape removes the project and allows you to pick a new one. Alt+P sets the current task name to the name of the project, so that you can create a new project from the collect page. Also, some bug fixes and project name auto-complete in the task edit page.</p>
<p>2012-07-24: Bunch of tiny updates - have been looking at profiling info, and will do more of that in the next days, I guess. Most things I've done are fixes to performance, but notice that you now get a counter to indicate how many items are in your inbox - right there on the process tab.</p>
<p>2012-05-28: Check out the offline tab for your contexts on the go! <a href="http://www.youtube.com/watch?v=IAb31rIeGZo" target="_blank">PocketMods</a> are a fun way to keep track of your actions when you're not around a computer.</p>
<p>2012-05-20: It's been there for a while, but check out the review page! It's not entirely done yet, but you can go through all the steps of a classic GTD review. Also, I've made an <a href="http://resources.gettodone.dk/gettodone.apk">app for collecting from your android phone</a>!</p>
<p>2012-04-14: You can now edit the title and project of a task on the do tab. People with more than one agenda item now only appear once on the agenda list.</p>
<p>2012-03-27: Added people administration - check out the People tab! Also, you can now mark a project as finished.</p>
<p>2012-03-18: Delegation now offers put in agenda and if you're set up correctly (try adding your email under /email, needs more work), send to inbox. Finish page now doesn't html encode your title after you click create. Calendar items are shown in your do list on the day they're due (and after). People you have active agenda items for are shown on your do page.</p>
<p>2012-02-17: Contexts now remember their state when you check/uncheck them.</p>
<p>2012-02-12: Project creation now has a page of its own, which allows you to create a next action along with the project.</p>
<p>2012-02-10: Added auto-completion of project names to the front of stuff.</p>
