// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function showRoleModal(name, title, department, location, jobDesc){
    $('#viewModalTitle').text(name);
    
    let body = `<dl class="row">`+ 
        `<dt class="col-sm-6">Job Title</dt>` +
        `<dd class="col-sm-6">${title}</dd>` +
        `<dt class="col-sm-6">Department</dt>` +
        `<dd class="col-sm-6">${department}</dd>` +
        `<dt class="col-sm-6">Location</dt>` +
        `<dd class="col-sm-6">${location}</dd>` +
        `<dt class="col-sm-6">Job Description</dt>` +
        `<dd class="col-sm-6">${jobDesc}</dd>` +
        `</dl>`;
    
    $('#viewModalBody').html(body);
    
    $('#viewModal').modal('show');
}