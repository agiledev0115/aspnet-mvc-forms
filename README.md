# example-aspnet-mvc-form

ASP.NET MVC Forms
https://teamtreehouse.com/library/aspnet-mvc-forms

- [HttpPost] in *EntriesController.cs* is called an attribute.
- `ModelState` contains the values of the properties with a list of error messages from the model.
	- With that in mind we can use something like `ModelState["date"].Value.AttemptedValue;` when someone puts in text for a date.
- *_EntryForm.cshtml* uses `@using (Html.BeginForm())` to create the view form and utilize the `ModelState` and HTML helper methods/functions not only in the view but in the controller.
```csharp
@using (Html.BeginForm())
{
// _EntryForm.cshtml
    @Html.ValidationSummary("The following errors were found:", new { @class = "alert alert-danger" })

    <div class="row">

        <div class="col-md-6">

            <div class="form-group">
                @Html.LabelFor(m => m.Date, new { @class = "control-label" }) // strongly typed, m is an anonymous function
                @Html.TextBoxFor(m => m.Date, "{0:d}", new { @class = "form-control datepicker" })
            </div>

...

// EntriesController.cs
 public ActionResult Edit(Entry entry)
        {
            ValidateEntry(entry);

            if (ModelState.IsValid)

...
```
- You can enforce a view to bind to a model and it will be *strongly typed*, so all the variables will be of Entry and bind to that.  *Entry.cs*
```csharp
// _EntryForm.cshtml
@model Treehouse.FitnessFrog.Models.Entry

@Html.LabelFor(m => m.Name, new { @class = "control-label" }) // strongly typed, m is an anonymous function
@Html.TextBoxFor(m => m.Name, new { @class = "form-control" })
```
- You can check if the binding is successful by doing `if (ModelState.IsValid)`

